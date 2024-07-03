using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Examples.y24.BackgroundTasks.QueueBackgroundService;

public class QueuedHostedService : IHostedLifecycleService
{
    private readonly ILogger<QueuedHostedService> _logger;
    public IBackgroundTaskQueue _taskQueue { get; }
    private readonly CancellationToken _cancellationToken;
    public QueuedHostedService(IBackgroundTaskQueue taskQueue,
        ILogger<QueuedHostedService> logger,
        IHostApplicationLifetime applicationLifetime)
    {
        _taskQueue = taskQueue;
        _logger = logger;
        _cancellationToken = applicationLifetime.ApplicationStopping;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(1);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(2);
    }

    public async Task StartingAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine(3);
    }

    public async Task StartedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(4);

        _logger.LogInformation(
            $"Queued Hosted Service is running.{Environment.NewLine}" +
            $"{Environment.NewLine}Tap W to add a work item to the " +
            $"background queue.{Environment.NewLine}");



        while (!_cancellationToken.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                var keyStroke = Console.ReadKey();

                if (keyStroke.Key == ConsoleKey.W)
                {
                    // Enqueue a background work item
                    await _taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItem);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("e");
            }
        }
    }

    private async ValueTask BuildWorkItem(CancellationToken token)
    {
        var guid = Guid.NewGuid().ToString();
        _logger.LogInformation("Queued Background Task {Guid} is starting.", guid);
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(1), token);
        }
        catch (OperationCanceledException)
        {
            // Prevent throwing if the Delay is cancelled
        }
    }

    public async Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Queued Hosted Service is stopping.");
    }

    public async Task StoppedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(6);
    }
}