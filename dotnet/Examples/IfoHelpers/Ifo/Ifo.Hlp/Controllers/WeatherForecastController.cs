using Microsoft.AspNetCore.Mvc;

namespace Ifo.Hlp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "API is running...";

        }
    }
}