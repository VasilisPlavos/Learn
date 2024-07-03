// See https://aka.ms/new-console-template for more information


using Examples.y24.BackgroundTasks.QueueBackgroundService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.BackgroundTasks;

static class Program
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
        }).ConfigureServices((context, services) =>
        {
            services.AddHostedService<ConsoleApp8Service>();
            services.AddHostedService<ConsoleApp6Service>();


            services.AddHostedService<QueuedHostedService>();
            services.AddHostedService<TimedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue>(ctx =>
            {
                if (!int.TryParse(context.Configuration["QueueCapacity"], out var capacity))
                {
                    capacity = 5;
                }

                return new BackgroundTaskQueue(capacity);
            });
        });
}