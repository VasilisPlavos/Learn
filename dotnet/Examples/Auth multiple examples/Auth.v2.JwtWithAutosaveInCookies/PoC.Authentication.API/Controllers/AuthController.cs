using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Helpers;
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

        [HttpPost("claim")]
        [Authorize]
        public async Task<bool> ClaimOwnership(string sourceJwtToMove)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var sourceToken = UtilHelper.GetToken(accessToken);
            var destinationToken = UtilHelper.GetToken(sourceJwtToMove);
            return await _authService.ClaimOwnershipAsync(sourceToken, destinationToken);
        }

        [HttpPost("register")]
        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            return await _authService.RegisterUserAsync(request);
        }

        [HttpPost("refresh")]
        public async Task<AccessOrRefreshResponse> RefreshUserToken(RefreshTokenDto refreshToken)
        {

            var tokenValue = await HttpContext.GetTokenAsync("access_token");
            var token = UtilHelper.GetToken(tokenValue);
            return await _authService.RefreshAuthorizedUserAsync(token, refreshToken.Value);
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<bool> RevokeUserToken(RefreshTokenDto refreshToken)
        {
            var tokenValue = await HttpContext.GetTokenAsync("access_token");
            var token = UtilHelper.GetToken(tokenValue);
            return await _authService.RevokeUserTokenAsync(token, refreshToken.Value);
        }





    }
}
