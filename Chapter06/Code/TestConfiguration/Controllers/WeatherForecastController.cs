using Microsoft.AspNetCore.Mvc;

namespace TestConfiguration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            //   return new string[] { "TestKey",
            //_configuration["TestKey"] };

            //return new string[] { "TestKeyFromAdditionalConfigJSON", _configuration["TestKeyFromAdditionalConfigJSON"] };

            return new string[] { "TestKeyFromAdditionalXMLConfig", _configuration["TestKeyFromAdditionalXMLConfig"] };

            //return new string[] { "TestSqlKey", _configuration["TestSqlKey"] };


        }
    }
}