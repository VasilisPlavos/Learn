using Azure.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace PoC.Authentication.API.Helpers;

public static class UtilHelper
{
    public static JwtSecurityToken? GetToken(string? jwt)
    {
        try { return new JwtSecurityToken(jwt); }
        catch (Exception) { return null; }
    }

    public static Guid GetUserId(JwtSecurityToken jwtSecurityToken)
    {
        var userIdString = jwtSecurityToken.Claims.Where(x => x.Type.ToLower() == "userid").Select(x => x.Value).FirstOrDefault();
        Guid.TryParse(userIdString, out var userId);
        return userId;
    }

    public static bool TokenIsExpired(JwtSecurityToken? jwtSecurityToken)
    {
        if (jwtSecurityToken == null) return false;
        return jwtSecurityToken.ValidTo < DateTime.UtcNow;
    }
}