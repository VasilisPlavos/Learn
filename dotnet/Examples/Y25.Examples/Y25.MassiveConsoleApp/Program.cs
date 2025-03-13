using Examples.y23.ImageSharp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Y25.ConsoleApp;

static class Program
{
    static async Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);
        var app = builder.Build();
        await app.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("connection");
                services.AddScoped<IImageSharpService, ImageSharpService>();
                services.AddHostedService<ConsoleApp8Service>();
            });
}