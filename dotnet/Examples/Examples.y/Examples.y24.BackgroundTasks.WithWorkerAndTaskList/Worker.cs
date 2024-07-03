using Microsoft.Extensions.Hosting;

namespace Examples.y24.BackgroundTasks.WithWorkerAndTaskList;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await CheckForTasks(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private async Task CheckForTasks(CancellationToken stoppingToken)
    {
        var tasksCount = Consts.TaskList.Count;
        Console.WriteLine($"{tasksCount} to complete!");
        //await Task.WhenAll(Consts.TaskList);
        Console.WriteLine($"{tasksCount} tasks completed!");
    }
}