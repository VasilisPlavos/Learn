using Microsoft.Extensions.Hosting;

namespace Examples.y24.BackgroundTasks;

public class ConsoleApp6Service : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ConsoleApp6Service)}:{nameof(StartAsync)}");
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ConsoleApp6Service)}:{nameof(StopAsync)}");
        await Task.CompletedTask;
    }
}