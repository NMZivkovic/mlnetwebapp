using MachineLearningWebApp.BikeSharingDemandData;
using MachineLearningWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MachineLearningWebApp.Controllers
{
    public class HomeController : Controller
    {
        public PredictionModel<BikeSharingDemandSample, BikeSharingDemandPrediction> _machineLearningModel;

        public HomeController()
        {
            _machineLearningModel = PredictionModel.ReadAsync<BikeSharingDemandSample, BikeSharingDemandPrediction>(@"./MachineLearningModel/Model.zip").Result;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Predict(
            float season,
            float year,
            float month,
            DateTime hour,
            bool holiday,
            float weekday,
            float weather,
            float temperature,
            float normalizedTemperature,
            float humidity,
            float windspeed)
        {
            var justHour = hour.Hour;

            var input = new BikeSharingDemandSample()
            {
                Season = season,
                Year = year,
                Month = month,
                Hour = justHour,
                Holiday = holiday,
                Weekday = weekday,
                Weather = weather,
                Temperature = temperature,
                NormalizedTemperature = normalizedTemperature,
                Humidity = humidity,
                Windspeed = windspeed,
                Count = 0
            };
            var prediction = _machineLearningModel.Predict(input);
            ViewData.Add("prediction", prediction.PredictedCount);
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
