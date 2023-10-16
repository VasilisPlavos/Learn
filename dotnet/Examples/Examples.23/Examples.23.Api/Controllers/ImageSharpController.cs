using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examples._23.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageSharpController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Api is running";
        }
    }
}
