using Examples._23.InParallel;
using Microsoft.AspNetCore.Mvc;

namespace Examples._23.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InParallelController : ControllerBase
    {
        private readonly IInParallelService _inParallelService;

        public InParallelController(IInParallelService inParallelService)
        {
            _inParallelService = inParallelService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _inParallelService.RunAsync();
            return "Api is running";
        }
    }
}
