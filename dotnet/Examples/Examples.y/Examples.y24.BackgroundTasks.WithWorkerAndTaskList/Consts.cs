using System.Collections.Concurrent;

namespace Examples.y24.BackgroundTasks.WithWorkerAndTaskList;

public class Consts
{
    public static ConcurrentBag<Task> TaskList { get; set; } = new();
}