using Examples.y24.BackgroundTasks.WithWorkerAndTaskList;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await CreateHostBuilder(args).RunConsoleAsync();
return;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    }).ConfigureServices((context, services) =>
    {
      //  services.AddHostedService<Worker>();
        services.AddHostedService<App>();
    });



