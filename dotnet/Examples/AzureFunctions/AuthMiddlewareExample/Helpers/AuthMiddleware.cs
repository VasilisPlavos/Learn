using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Net;

namespace HelloWorldCode.Helpers;

public class AuthMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (!TokenHelper.TryGetFromHeaders(context, out var token))
        {
            context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
        }

        Console.WriteLine("AuthMw: Do stuff before function");
        await next(context);
        Console.WriteLine("AuthMw: Do stuff after function");
    }
}