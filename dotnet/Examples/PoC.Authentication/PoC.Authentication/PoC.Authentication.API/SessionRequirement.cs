using Microsoft.AspNetCore.Authorization;

namespace PoC.Authentication.API;

public class SessionRequirement : IAuthorizationRequirement
{
    public SessionRequirement(string sessionHeaderName)
    {
        SessionHeaderName = sessionHeaderName;
    }
    public string SessionHeaderName { get; }
}

public class SessionHandler : AuthorizationHandler<SessionRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SessionHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task HandleRequirementAsync
        (AuthorizationHandlerContext context, SessionRequirement requirement)
    {
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        if (!httpRequest.Headers[requirement.SessionHeaderName].Any())
        {
            context.Fail();
            return Task.CompletedTask;
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}