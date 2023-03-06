using Net6_QueueListWithPriorities.DTOs;

namespace Net6_QueueListWithPriorities;

public static class ExampleWithPriorityQueuesAndItemsInQueues
{
    public static async Task RunAsync()
    {
        // This solution is FIFO. 26 comes after 21
        Console.WriteLine("This solution is FIFO. 26 comes after 21");

        var cts = new CancellationTokenSource();
        var queue = new PriorityQueue<QueueItem, double>();

        queue.Enqueue(new QueueItem { Name = "21", PriorityTitle = "Normal" }, 2.1);
        queue.Enqueue(new QueueItem { Name = "02", PriorityTitle = "Platinum" }, 0.1);
        queue.Enqueue(new QueueItem { Name = "13", PriorityTitle = "Gold" }, 1.1);
        queue.Enqueue(new QueueItem { Name = "14", PriorityTitle = "Gold" }, 1.2);
        queue.Enqueue(new QueueItem { Name = "05", PriorityTitle = "Platinum" }, 0.2);
        queue.Enqueue(new QueueItem { Name = "26", PriorityTitle = "Normal" }, 2.2);

        while (!cts.IsCancellationRequested)
        {
            await Task.Delay(1000, cts.Token);
            if (queue.Count > 0)
            {
                var x = queue.Dequeue();
                Console.WriteLine($"{x.PriorityTitle}: {x.Name}");
                continue;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            cts.Cancel();
        }
    }
}