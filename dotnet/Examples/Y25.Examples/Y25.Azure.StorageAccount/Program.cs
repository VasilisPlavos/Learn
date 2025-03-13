using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Y25.Azure.StorageAccount.Services;

namespace Y25.Azure.StorageAccount;

static class Program
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
        })
            .ConfigureServices((hostContext, services) =>
            {
                var blobConnectionString = hostContext.Configuration.GetConnectionString("BlobConnection");
                services.AddScoped<IStorageAccountService, StorageAccountService>();
                services.AddSingleton(u => new BlobServiceClient(blobConnectionString));
                services.AddHostedService<ConsoleApp8Service>();
            });
}