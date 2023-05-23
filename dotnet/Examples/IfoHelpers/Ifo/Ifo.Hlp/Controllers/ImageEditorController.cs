using Ifo.Hlp.Programs;
using Microsoft.AspNetCore.Mvc;

namespace Ifo.Hlp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageEditorController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            await ImageEditorProgram.TestAsync();
            return "API is running...";
        }
    }
}
