using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace PoC.Authentication.API.Helpers;

public class UtilHelper
{
    private static string GetToken(HttpRequest request)
    {
        var authHeader = request.Headers["Authorization"];
        AuthenticationHeaderValue.TryParse(authHeader, out var headerValue);
        return headerValue?.Parameter;
    }

    public static Guid GetUserId(string jwt)
    {
        var jwtSecurityToken = new JwtSecurityToken(jwt);
        var userIdString = jwtSecurityToken.Claims.Where(x => x.Type.ToLower() == "userid").Select(x => x.Value).FirstOrDefault();
        Guid.TryParse(userIdString, out var userId);
        return userId;
    }

    internal static Guid GetUserId(HttpRequest request)
    {
        var jwt = GetToken(request);
        return GetUserId(jwt);
    }

    public static bool TokenIsExpired(HttpRequest request)
    {
        //var a = this.User.FindFirstValue("exp");
        var jwt = GetToken(request);
        JwtSecurityToken jwtSecurityToken;
        try
        {
            jwtSecurityToken = new JwtSecurityToken(jwt);
        }
        catch (Exception)
        {
            return false;
        }

        return jwtSecurityToken.ValidTo < DateTime.UtcNow;
    }
}