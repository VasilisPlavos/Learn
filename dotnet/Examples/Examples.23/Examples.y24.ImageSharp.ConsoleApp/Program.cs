using Examples._23.ImageSharp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.ImageSharp.ConsoleApp;

static class Program
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var connectionStringExample = hostContext.Configuration.GetConnectionString("connection");
                Console.WriteLine($"{connectionStringExample}");
                
                services.AddScoped<IImageSharpService, ImageSharpService>();

                services.AddHostedService<ConsoleAppService>();
            });
}