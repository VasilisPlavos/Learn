using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace HelloWorldCode.Helpers.Middlewares;

public class SampleMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        Console.WriteLine("SampleMW: DoStuffBeforeFunction");
        await next(context);
        Console.WriteLine("SampleMW: DoStuffAfterFunction");
    }
}