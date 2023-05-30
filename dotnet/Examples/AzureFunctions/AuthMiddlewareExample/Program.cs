using HelloWorldCode.Helpers.Middlewares;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        builder.UseMiddleware<SampleMiddleware>();
        builder.UseMiddleware<AuthorizationMiddleware>();
        builder.UseMiddleware<AuthenticationMiddleware>();
    })
    .Build();

host.Run();
