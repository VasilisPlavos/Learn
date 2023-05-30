using HelloWorldCode.Helpers;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        builder.UseMiddleware<AuthMiddleware>();
    })
    .Build();

host.Run();
