using Microsoft.AspNetCore.Authentication;
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
            var tokenValue = await HttpContext.GetTokenAsync("access_token");
            var token = UtilHelper.GetToken(tokenValue);
            if (UtilHelper.TokenIsExpired(token))
            {
                return BadRequest(StatusCodes.Status401Unauthorized);
            }
            return await _projectsService.CreateUserProjectAsync(token);
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetUserProjects()
        {
            var tokenValue = await HttpContext.GetTokenAsync("access_token");
            var token = UtilHelper.GetToken(tokenValue);
            if (UtilHelper.TokenIsExpired(token))
            {
                return BadRequest(StatusCodes.Status401Unauthorized);
            }

            return await _projectsService.GetUserProjectsAsync(token);
        }
    }
}
