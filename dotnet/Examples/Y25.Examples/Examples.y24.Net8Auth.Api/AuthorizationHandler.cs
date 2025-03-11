using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace Examples.y24.Net8Auth.Api;

// I can use something like this, to revoke tokens
public class AuthorizationHandler : IAuthorizationHandler
{
    // TODO: It is needed a method that remove the expired tokens from here
    // KeyToRevoke, DateTimeExpires
    private static readonly ConcurrentDictionary<string, DateTime> RevokedTokens = new();

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
       //TestRevoke(context);
        try
        {
            var httpContext = context.Resource as HttpContext;
            var token = httpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            if (RevokedTokens.ContainsKey(token))
            {
                context.Fail();
            }
        }
        catch
        {
            // ignored
        }

        return Task.CompletedTask;
    }

    private void TestRevoke(AuthorizationHandlerContext context)
    {
        var httpContext = context.Resource as HttpContext;
        var token = httpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
        var dateExpires = DateTime.UtcNow.AddHours(1); // TODO: I have to fix the expiration time
        Revoke(token, dateExpires);
    }

    private void Revoke(string token, DateTime dateExpires)
    {
        RevokedTokens.TryAdd(token, dateExpires);
    }
}