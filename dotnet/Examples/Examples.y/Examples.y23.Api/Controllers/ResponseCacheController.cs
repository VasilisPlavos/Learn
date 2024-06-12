using Microsoft.AspNetCore.Mvc;

namespace Examples.y23.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseCacheController : ControllerBase
    {
        [HttpGet]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100, VaryByQueryKeys = new[] { "*" })]
        public async Task<string> Get(int value)
        {
            return $"value is {value}";
        }
    }
}