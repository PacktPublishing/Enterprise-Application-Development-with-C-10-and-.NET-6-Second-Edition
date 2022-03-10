// "//-----------------------------------------------------------------------".
// <copyright file="Program.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

using Microsoft.Azure.Cosmos;
using Packt.Ecommerce.Common.Options;
using Packt.Ecommerce.DataAccess.Extensions;
using Packt.Ecommerce.DataStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
builder.Services.Configure<DatabaseSettingsOptions>(builder.Configuration.GetSection("CosmosDB"));
string accountEndPoint = builder.Configuration.GetValue<string>("CosmosDB:AccountEndPoint");
string authKey = builder.Configuration.GetValue<string>("CosmosDB:AuthKey");
CosmosClientOptions options = new ()
{
    SerializerOptions = new () { IgnoreNullValues = true },
};
builder.Services.AddSingleton(s => new CosmosClient(accountEndPoint, authKey, options));
builder.Services.AddRepositories();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
