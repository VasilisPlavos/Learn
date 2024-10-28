using Microsoft.Extensions.Hosting;

namespace Examples.y24.BackgroundHostedServices;

public class ConsoleApp8Service : IHostedLifecycleService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartAsync));
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