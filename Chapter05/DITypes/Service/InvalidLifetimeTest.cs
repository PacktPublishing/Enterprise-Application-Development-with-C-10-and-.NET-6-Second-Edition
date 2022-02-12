using DITypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DITypes.Service
{
    public class InvalidLifetimeTest : IInvalidLifetimeTest
    {
        public InvalidLifetimeTest(IWeatherProvider weatherProvider)
        {
            WeatherProvider = weatherProvider;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IWeatherProvider WeatherProvider { get; }

        public IEnumerable<WeatherForecast> GetForecastOfLocation(string location)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Location = location,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
