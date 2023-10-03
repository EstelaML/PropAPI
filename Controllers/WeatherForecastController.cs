using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<dynamic> Get()
        {
            /*   return Enumerable.Range(1, 5).Select(index => new WeatherForecast
               {
                   Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                   TemperatureC = Random.Shared.Next(-20, 55),
                   Summary = Summaries[Random.Shared.Next(Summaries.Length)]
               })
               .ToArray();*/
            using (PropBDContext ctx = new PropBDContext()) {
                
                var l = ctx.Usuario.Include(b => b.IdSeguido).Select(o => new { o.NickName, o.IdSeguidor }).ToList();
                return l;
            }

        }
    }
}