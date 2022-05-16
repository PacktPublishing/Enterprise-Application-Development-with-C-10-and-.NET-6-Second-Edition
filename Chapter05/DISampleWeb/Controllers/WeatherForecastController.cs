using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DISampleWeb.Models;
using DISampleWeb.Services;

namespace DISampleWeb.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnumerable<IWeatherForcastService> weatherForcastServices;
        //private readonly IWeatherForcastService weatherForcastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            //This will retrieve the last registration to IWeatherForcastService
            //IWeatherForcastService weatherForcastService

            //Use IEnumerable to inject all the services registered with IWeatherForcastService
            IEnumerable<IWeatherForcastService> weatherForcastServices)
        {
            _logger = logger;
            //this.weatherForcastService = weatherForcastService;
            this.weatherForcastServices = weatherForcastServices;
        }

      
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
