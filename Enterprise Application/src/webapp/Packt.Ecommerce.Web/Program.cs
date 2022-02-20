// "//-----------------------------------------------------------------------".
// <copyright file="Program.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
using Microsoft.ApplicationInsights.SnapshotCollector;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Newtonsoft.Json;
using Packt.Ecommerce.Common.Options;
using Packt.Ecommerce.Web;
using Packt.Ecommerce.Web.Contracts;
using Packt.Ecommerce.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IECommerceService, ECommerceService>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(Policies.RetryPolicy()) // Retry policy
    .AddPolicyHandler(Policies.CircuitBreakerPolicy()); // Circuit breakerpolicy

builder.Services.AddScoped<IECommerceService, ECommerceService>();

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

builder.Services.AddSnapshotCollector((configuration) => builder.Configuration.Bind(nameof(SnapshotCollectorConfiguration), configuration));

// Add health check services to the container.
builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri(builder.Configuration.GetValue<string>("ApplicationSettings:ProductsApiEndpoint")), name: "Product Service")
    .AddUrlGroup(new Uri(builder.Configuration.GetValue<string>("ApplicationSettings:OrdersApiEndpoint")), name: "Order Service")
    .AddProcessMonitorHealthCheck("notepad", name: "Notepad monitor");

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
   .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddRazorPages().AddMicrosoftIdentityUI();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Products/Error/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Products/Error/500");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Products}/{action=Index}/{id?}");

    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                Status = report.Status.ToString(),
                HealthChecks = report.Entries.Select(x => new
                {
                    Component = x.Key,
                    Status = x.Value.Status.ToString(),
                    Description = x.Value.Description,
                }),
                HealthCheckDuration = report.TotalDuration,
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response)).ConfigureAwait(false);
        },
    });
});

app.Run();
