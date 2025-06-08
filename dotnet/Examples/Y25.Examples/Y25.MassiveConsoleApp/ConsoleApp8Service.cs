using Examples.y23.ImageSharp.Services;
using Microsoft.Extensions.Hosting;
using Y25.ManyProcessors.Processors;

namespace Y25.ConsoleApp;

public class ConsoleApp8Service : IHostedLifecycleService
{
    private readonly IImageSharpService _imageSharpService;
    private readonly ITransactionService _transactionService;

    public ConsoleApp8Service(IImageSharpService imageSharpService, ITransactionService transactionService)
    {
        _imageSharpService = imageSharpService;
        _transactionService = transactionService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartAsync));
        await _imageSharpService.RunAsync();
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
        await _transactionService.RunTransactionAsync(cancellationToken);
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