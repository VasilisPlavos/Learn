using PoC.Authentication.API.Helpers.Middlewares;

namespace PoC.Authentication.API;

public static class CustomMiddlewaresRegistry
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}