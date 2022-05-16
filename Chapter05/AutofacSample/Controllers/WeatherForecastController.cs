using DITypes.Models;
using DITypes.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherProvider weatherProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherProvider weatherProvider)
        {
            _logger = logger;
            this.weatherProvider = weatherProvider;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return weatherProvider.GetForecastOfLocation("location");
        }
    }
}
