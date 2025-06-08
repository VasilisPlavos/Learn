using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Services;
using System.IdentityModel.Tokens.Jwt;

namespace PoC.Authentication.API.Helpers;

public class JwtBearerEventsHandler : JwtBearerEvents
{
	private readonly IAuthService _authService;

	public JwtBearerEventsHandler(IAuthService authService)
	{
		_authService = authService;
		OnMessageReceived = async context => { await MessageReceivedHandler(context); };
	}

	private async Task MessageReceivedHandler(MessageReceivedContext context)
    {
        TryGetToken(context, out var accessToken);
        if (accessToken == null)
        {
            var accessRequest = new AccessRequest { Anonymous = true };
            var accessResponse = await _authService.AccessAuthenticatedOrAnonymousUserAsync(accessRequest);
            accessToken = UtilHelper.GetToken(accessResponse.access_token);
            context.Request.Headers.Authorization = accessResponse.access_token;
        }

        context.HttpContext.Response.Cookies.Append("X-Access-Token", $"{accessToken.RawData}", new CookieOptions { HttpOnly = true, Expires = accessToken.ValidTo, SameSite = SameSiteMode.Strict });
        context.Token = $"{accessToken.RawData}";
	}

    private bool TryGetToken(MessageReceivedContext context, out JwtSecurityToken? jwt)
    {
        jwt = null;
        string jwtValue;
        if (!string.IsNullOrEmpty(context.Token))
        {
            jwt = UtilHelper.GetToken(context.Token);
            return true;
        }

        if (context.Request.Headers.TryGetValue("authorization", out var authStringValues))
        {
            var authString = authStringValues.ToString();
            if (authString.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                jwtValue = authString["Bearer ".Length..].Trim();
                jwt = UtilHelper.GetToken(jwtValue);
                return true;
            }
        }

        if (context.Request.Cookies.TryGetValue("X-Access-Token", out jwtValue))
        {
            jwt = UtilHelper.GetToken(jwtValue);
            return true;
        }
        return false;
    }
}