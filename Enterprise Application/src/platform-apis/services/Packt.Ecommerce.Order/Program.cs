// "//-----------------------------------------------------------------------".
// <copyright file="Program.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

using Packt.Ecommerce.Caching;
using Packt.Ecommerce.Caching.Interfaces;
using Packt.Ecommerce.Common.Middlewares;
using Packt.Ecommerce.Common.Options;
using Packt.Ecommerce.Order;
using Packt.Ecommerce.Order.Contracts;
using Packt.Ecommerce.Order.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services.AddHttpClient<IOrderService, OrdersService>()
.SetHandlerLifetime(TimeSpan.FromMinutes(5))
.AddPolicyHandler(RetryPolicy()) // Retry policy.
.AddPolicyHandler(CircuitBreakerPolicy()); // Circuit breakerpolicy

builder.Services.AddScoped<IOrderService, OrdersService>();

// Inject Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Inject Cache service
builder.Services.AddSingleton<IEntitySerializer, EntitySerializer>();
builder.Services.AddSingleton<IDistributedCacheService, DistributedCacheService>();

if (builder.Configuration.GetValue<bool>("ApplicationSettings:UseRedis"))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
}

// App insights.
string appinsightsInstrumentationKey = builder.Configuration.GetValue<string>("ApplicationSettings:InstrumentationKey");

if (!string.IsNullOrWhiteSpace(appinsightsInstrumentationKey))
{
    builder.Services.AddLogging(logging =>
    {
        logging.AddApplicationInsights(appinsightsInstrumentationKey);
    });
    builder.Services.AddApplicationInsightsTelemetry(appinsightsInstrumentationKey);
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// The Retry policy.
/// </summary>
/// <returns>HttpResponseMessage.</returns>
static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
{
    Random random = new Random();
    var retryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(
        5,
        retry => TimeSpan.FromSeconds(Math.Pow(2, retry))
                          + TimeSpan.FromMilliseconds(random.Next(0, 100)));
    return retryPolicy;
}

/// <summary>
/// Gets the circuit breaker policy.
/// </summary>
/// <returns>HttpResponseMessage.</returns>
static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}