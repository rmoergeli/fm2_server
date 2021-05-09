using fm2_server.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fm2_server.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        #region Properties
        private readonly ILogger<WeatherViewModel> _logger;

        private readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private WeatherForecast[] weatherForecasts;
        public WeatherForecast[] WeatherForecasts
        {
            get => weatherForecasts;
            private set
            {
                weatherForecasts = value;
                OnPropertyChanged(nameof(WeatherForecasts));
            }
        }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Constructor(s)
        /// </summary>
        public WeatherViewModel(ILogger<WeatherViewModel> logger)
        {
            RetrieveForecasts().Wait();
        }
        #endregion

        private async Task RetrieveForecasts()
        {
            WeatherForecasts = await GetForecastAsync(DateTime.Now);
        }

        private Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
