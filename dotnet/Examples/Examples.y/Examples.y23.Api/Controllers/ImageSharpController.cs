using Examples._23.ImageSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examples._23.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageSharpController : ControllerBase
    {
        private readonly IImageSharpService _imageSharpService;

        public ImageSharpController(IImageSharpService imageSharpService)
        {
            _imageSharpService = imageSharpService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _imageSharpService.RunAsync();
            return "Api is running";
        }
    }
}
