using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestEntries.Contracts.Contracts.Joke;
using TestEntries.Processors.Processors;

namespace TestEntries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEntriesController : Controller
    {
        // GET /api/TestEntries
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("TestEntriesAPI running...");
        }

        // POST /api/TestEntries/get
        [HttpPost("post")]
        //[ServiceFilter(typeof(ActionFilterAttribute))]
        [ProducesResponseType(typeof(JokeResponsePackage), 200)]
        public async Task<IActionResult> GetRandomJoke([FromBody] JokeRequestPackage request)
        {
            return await GetRandomJokeProcessor.GetResponseAsync(request);
        }
    }
}
