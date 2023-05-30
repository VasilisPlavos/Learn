using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace HelloWorldCode.Helpers;

public class AuthMw : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        Console.WriteLine("AuthMw: Do stuff before function");
        await next(context);
        Console.WriteLine("AuthMw: Do stuff after function");
    }
}