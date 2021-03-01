using foodbooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace foodbooks.Controllers
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

        [HttpGet] [Authorize(Roles ="Admin")]
        public IEnumerable<WeatherForecast> Get()
        {
          //  var stream = Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
          //  var handler = new JwtSecurityTokenHandler();
          //  var jsonToken = handler.ReadToken(stream);
          //  var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
          //TokenPayLoad payload=JsonConvert.DeserializeObject<TokenPayLoad>(tokenS.Payload.SerializeToJson());
          //  User.FindFirst(ClaimTypes.name)
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                //tokenvalue = payload.userInfo.id
            })
            .ToArray();
        }
       
    }
}
