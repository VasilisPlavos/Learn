using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseCacheController : ControllerBase
    {
        [HttpGet]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100, VaryByQueryKeys = new[] { "*" })]
        public async Task<string> GetAsync(int value)
        {
            return $"value is {value}";
        }
    }
}