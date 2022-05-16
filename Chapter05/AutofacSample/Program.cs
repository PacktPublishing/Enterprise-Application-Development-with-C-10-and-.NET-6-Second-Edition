using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSample.Services;
using DITypes.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Call UseServiceProviderFactory to register Autofac provider factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host property of WrbApplicationBuilder 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<WeatherProvider>()
                    .As<IWeatherProvider>();
});

builder.Services.AddControllers();
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
