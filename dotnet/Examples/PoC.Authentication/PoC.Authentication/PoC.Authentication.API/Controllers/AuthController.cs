using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Services;

namespace PoC.Authentication.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("API is running...");
        }
        
        [HttpPost("access")]
        public async Task<AccessOrRefreshResponse> AccessAuthenticatedOrAnonymousUser(AccessRequest request)
        {
            return await _authService.AccessAuthenticatedOrAnonymousUserAsync(request);
        }

        [HttpPost("register")]
        public async Task<bool> CreateUser(RegisterRequest request)
        {
            return await _authService.CreateUserAsync(request);
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<AccessOrRefreshResponse> RefreshUserToken(RefreshTokenDto refreshToken)
        {
            return await _authService.RefreshAuthorizedUserAsync(HttpContext.Request, refreshToken.Value);
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<bool> RevokeUserToken(RefreshTokenDto refreshToken)
        {
            return await _authService.RevokeUserTokenAsync(HttpContext.Request, refreshToken.Value);
        }





    }
}
