using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Net;

namespace HelloWorldCode.Helpers.Middlewares;

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (!TokenHelper.TryGetFromHeaders(context, out var token))
        {
            // Unable to get token from headers
            context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
            return;
        }

        Console.WriteLine("AuthMw: Do stuff before function");
        await next(context);
        Console.WriteLine("AuthMw: Do stuff after function");
    }
}