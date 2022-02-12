using DITypes.Models;
using System.Collections.Generic;

namespace DITypes.Service
{
    public interface IInvalidLifetimeTest
    {
        IEnumerable<WeatherForecast> GetForecastOfLocation(string location);
    }
}
