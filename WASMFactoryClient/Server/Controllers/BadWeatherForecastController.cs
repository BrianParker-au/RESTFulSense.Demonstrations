using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WASMFactoryClient.Shared;

namespace WASMFactoryClient.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BadWeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public BadWeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return BadRequest();
        }
    }
}