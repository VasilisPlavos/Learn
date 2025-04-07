using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Y25.Database;
using Y25.ExecuteUpdateAsync;
using Y25.ManyProcessors;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);
        var app = builder.Build();
        await app.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddDbServices(hostContext.Configuration);
            services.AddManyProcessorsServices(hostContext.Configuration);
            services.AddHostedService<ConsoleApp8Service>();
        });
    }
}