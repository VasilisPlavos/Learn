using Microsoft.Extensions.Hosting;
using Y25.Azure.StorageAccount.ConsoleApp.Services;

namespace Y25.Azure.StorageAccount.ConsoleApp;

public class ConsoleApp8Service : IHostedLifecycleService
{
    private readonly IStorageAccountService _storageAccountService;

    public ConsoleApp8Service(IStorageAccountService storageAccountService)
    {
        _storageAccountService = storageAccountService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartAsync));
        await _storageAccountService.RunAsync();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StopAsync));
        await Task.CompletedTask;
    }

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartingAsync));
        await Task.CompletedTask;
    }

    public async Task StartedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartedAsync));
        await Task.CompletedTask;
    }

    public async Task StoppingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppingAsync));
        await Task.CompletedTask;
    }

    public async Task StoppedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppedAsync));
        await Task.CompletedTask;
    }
}