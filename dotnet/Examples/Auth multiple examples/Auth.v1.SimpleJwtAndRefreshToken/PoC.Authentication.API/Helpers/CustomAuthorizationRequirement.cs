using Microsoft.AspNetCore.Authorization;

namespace PoC.Authentication.API.Helpers;

public class CustomAuthorizationRequirement : IAuthorizationRequirement
{
    public CustomAuthorizationRequirement(string sessionHeaderName)
    {
        SessionHeaderName = sessionHeaderName;
    }
    public string SessionHeaderName { get; }
}

public class SessionHandler : AuthorizationHandler<CustomAuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SessionHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task HandleRequirementAsync
        (AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
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