using Examples.y24.HttpClientExamples.Dtos.Joke;
using Examples.y24.HttpClientExamples.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.HttpClientExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokesService _jokesService;

        public JokesController(IJokesService jokesService)
        {
            _jokesService = jokesService;
        }

        [HttpGet]
        public async Task<JokesResults> GetJokeBySearchQuery(string q = "cats")
        {
            return await _jokesService.GetJokeBySearchQueryAsync(q);
        }

        [HttpGet("[action]")]
        public async Task<Joke> GetRandomJoke1()
        {
            return await _jokesService.GetRandomJoke1Async();
        }

        [HttpGet("[action]")]
        public async Task<Joke> GetRandomJoke2()
        {
            return await _jokesService.GetRandomJoke2Async();
        }

        [HttpGet("[action]")]
        public async Task<Joke> GetRandomJoke3()
        {
            return await _jokesService.GetRandomJoke3Async();
        }
    }
}
