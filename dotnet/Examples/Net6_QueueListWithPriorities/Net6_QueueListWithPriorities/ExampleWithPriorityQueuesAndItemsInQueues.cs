using Net6_QueueListWithPriorities.DTOs;

namespace Net6_QueueListWithPriorities;

public static class ExampleWithPriorityQueuesAndItemsInQueues
{
    public static async Task RunAsync()
    {
        // This solution is FIFO. 36 comes after 31
        Console.WriteLine("This solution is FIFO. 36 comes after 31");

        var cts = new CancellationTokenSource();
        var queue = new PriorityQueue<QueueItem, double>();

        queue.Enqueue(new QueueItem { Name = "31", PriorityTitle = "Normal" }, 3.1);
        queue.Enqueue(new QueueItem { Name = "12", PriorityTitle = "Platinum" }, 1.1);
        queue.Enqueue(new QueueItem { Name = "23", PriorityTitle = "Gold" }, 2.1);
        queue.Enqueue(new QueueItem { Name = "24", PriorityTitle = "Gold" }, 2.2);
        queue.Enqueue(new QueueItem { Name = "15", PriorityTitle = "Platinum" }, 1.2);
        queue.Enqueue(new QueueItem { Name = "36", PriorityTitle = "Normal" }, 3.2);

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