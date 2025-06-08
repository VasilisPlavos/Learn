using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoC.Authentication.API.Entities;
using PoC.Authentication.API.Helpers;
using PoC.Authentication.API.Services;

namespace PoC.Authentication.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateUserProject()
        {
            if (UtilHelper.TokenIsExpired(HttpContext.Request.Headers))
            {
                return BadRequest(StatusCodes.Status401Unauthorized);
            }
            return await _projectsService.CreateUserProjectAsync(HttpContext.Request);
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetUserProjects()
        {
            if (UtilHelper.TokenIsExpired(HttpContext.Request.Headers))
            {
                return BadRequest(StatusCodes.Status401Unauthorized);
            }

            return await _projectsService.GetUserProjectsAsync(HttpContext.Request);
        }
    }
}
