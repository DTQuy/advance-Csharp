using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    /// <summary>
    /// WeatherForecastController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// Summaries
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// WeatherForecastController
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// WeatherForecast
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}