using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples.y24.BackgroundTasks.QueueBackgroundService;

public class TimedHostedService : BackgroundService
{
    private readonly ILogger<TimedHostedService> _logger;
    private int _executionCount;
    public IBackgroundTaskQueue _taskQueue { get; }

    public TimedHostedService(IBackgroundTaskQueue taskQueue, ILogger<TimedHostedService> logger)
    {
        _taskQueue = taskQueue;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
        }
    }

    // Could also be an async method, that can be awaited in ExecuteAsync above
    private async Task DoWork(CancellationToken stoppingToken)
    {
        int count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation("Timed Hosted Service: {Count}", count);

        // TODO: I have to fix this! It does the job but it stops the loop
        await _taskQueue.ExecuteAsync(stoppingToken);
        return;

        //_logger.LogInformation("Timed Hosted Service: after _taskQueue.ExecuteAsync(stoppingToken)");


    }
}