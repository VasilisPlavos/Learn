using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.BackgroundTasks.WithWorkerAndTaskList;

public class App : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var key = Console.ReadKey();
        while (key.Key != ConsoleKey.Escape)
        {
            Consts.TaskList.Add(DoAsync(key.KeyChar));
            key = Console.ReadKey();
        }

    }

    static async Task DoAsync(char keyKeyChar)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        Console.Write(keyKeyChar);
    }
}