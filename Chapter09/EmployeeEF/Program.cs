using System;
using EmployeeEF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<EmployeeContext>(options =>
{ 
 options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeContext"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if(app.Environment.IsDevelopment())
{
    using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope())
    {
                    using (var context = serviceScope?.ServiceProvider.GetRequiredService<EmployeeContext>())
                    {
                            context?.SeedData();
                    }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
