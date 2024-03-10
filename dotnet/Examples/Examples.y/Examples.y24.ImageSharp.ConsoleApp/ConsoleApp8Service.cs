using Microsoft.Extensions.Hosting;

namespace Examples.y24.ImageSharp.ConsoleApp;

public class ConsoleApp8Service : IHostedLifecycleService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartAsync));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StopAsync));
    }

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartingAsync));
    }

    public async Task StartedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartedAsync));
    }

    public async Task StoppingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppingAsync));
    }

    public async Task StoppedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppedAsync));
    }
}