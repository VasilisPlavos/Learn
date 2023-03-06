using System.Diagnostics;
using Net6_QueueListWithPriorities.DTOs;
using Net6_QueueListWithPriorities.Enums;

namespace Net6_QueueListWithPriorities;

public static class ExampleWithPriorityQueuesAndItemsInQueuesBetterApproach
{
    public static async Task RunAsync()
    {
	    // This solution is FIFO. 26 comes after 21
	    Console.WriteLine("This solution is FIFO. 26 comes after 21");

		var cts = new CancellationTokenSource();
        var queue = new PriorityQueue<QueueItem, (Status, DateTime)>();

		queue.Enqueue(new QueueItem { Name = "21", PriorityTitle = "Normal" }, (Status.Normal, DateTime.UtcNow));
        queue.Enqueue(new QueueItem { Name = "02", PriorityTitle = "Platinum" }, (Status.Platinum, DateTime.UtcNow));
        queue.Enqueue(new QueueItem { Name = "13", PriorityTitle = "Gold" }, (Status.Gold, DateTime.UtcNow));
        queue.Enqueue(new QueueItem { Name = "14", PriorityTitle = "Gold" }, (Status.Gold, DateTime.UtcNow));
        queue.Enqueue(new QueueItem { Name = "05", PriorityTitle = "Platinum" }, (Status.Platinum, DateTime.UtcNow));
        queue.Enqueue(new QueueItem { Name = "26", PriorityTitle = "Normal" }, (Status.Normal, DateTime.UtcNow));

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