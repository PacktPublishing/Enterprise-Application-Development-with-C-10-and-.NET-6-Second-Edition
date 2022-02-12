using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DITypes.Models;
using DITypes.Service;

namespace DITypes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService weatherService;
        private readonly IInvalidLifetimeTest invalidLifetimeTest;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService
            /*
             * Uncommenting this will throw an exception 
             * InvalidOperationException: Cannot consume scoped service 'DITypes.Service.IWeatherProvider' from singleton 'DITypes.Service.IInvalidLifetimeTest'.
             * Due to lifetime validation 
            , IInvalidLifetimeTest invalidLifetimeTest
            */
            , IInvalidLifetimeTest invalidLifetimeTest
            )
        {
            _logger = logger;
            this.weatherService = weatherService;
            this.invalidLifetimeTest = invalidLifetimeTest;
        }

        public IActionResult Constructor()
        {
            return View(weatherService.GetForeCast("Hyderabad"));
        }

        public IActionResult Property()
        {
            WeatherService_PropertyInection weatherService = new WeatherService_PropertyInection();
            weatherService.WeatherProvider = new WeatherProvider();
            return View(weatherService.GetForeCast("Hyderabad"));
        }

        public IActionResult Method()
        {
            WeatherService_MethodInection weatherService = new WeatherService_MethodInection();
            var weatherProvider = new WeatherProvider();
            return View(weatherService.GetForeCast("Hyderabad", weatherProvider));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
