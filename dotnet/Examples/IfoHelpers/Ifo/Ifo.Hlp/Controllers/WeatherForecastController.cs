using Ifo.Hlp.Helpers;
using Ifo.Hlp.Programs;
using Microsoft.AspNetCore.Mvc;

namespace Ifo.Hlp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            return "API is running...";
        }
    }
}