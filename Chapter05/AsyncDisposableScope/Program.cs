using AsyncDisposableScope;
using Microsoft.Extensions.DependencyInjection;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

await using var provider = new ServiceCollection()
    .AddScoped<IWeatherProviderAsync, WeatherProviderAsync>()
    .BuildServiceProvider();

await using (var scope = provider.CreateAsyncScope())
{
    var weather = scope.ServiceProvider.GetRequiredService<IWeatherProviderAsync>();
    Console.WriteLine($"Current tmperature is {weather.GetCurrentTemperatire()}F");
} 

