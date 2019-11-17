using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForecastApp.ForecastAppModels;
using ForecastApp.OpenWeatherMapModels;
using ForecastApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApp.Controllers
{ 
    public class ForecastAppController : Controller
    {
        private readonly IForecastRepository _forecastAppRepository;
        
        //Dependency Injection
        public ForecastAppController(IForecastRepository forecastAppRepository)
        {
            _forecastAppRepository = forecastAppRepository;
        }

        //GET: ForecastApp/SearchCity
        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        //POST: ForecastApp/SearchCity
        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "ForecastApp", new {city = model.CityName});
            }

            return View(model);
        }

        //GET: ForecastApp/City
        public IActionResult City(string city)
        {
            WeatherResponse weatherResponse = _forecastAppRepository.GetForecast(city);

            City viewModel = new City();

            if (weatherResponse != null)
            {
                viewModel.Name = weatherResponse.Name;
                viewModel.Humidity = weatherResponse.Main.Humidity;
                viewModel.Pressure = weatherResponse.Main.Pressure;
                viewModel.Temp = weatherResponse.Main.Temp;
                viewModel.Weather = weatherResponse.Weather[0].Main;
                viewModel.Wind = weatherResponse.Wind.Speed;
            }

            return View(viewModel);
        }
    }
}