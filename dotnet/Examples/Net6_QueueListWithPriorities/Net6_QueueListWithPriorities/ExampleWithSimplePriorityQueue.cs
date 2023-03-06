using Net6_QueueListWithPriorities.DTOs;

namespace Net6_QueueListWithPriorities;

public static class ExampleWithSimplePriorityQueue
{
	public static async Task RunAsync()
	{
		// This solution is not FIFO. 26 comes before 21
		Console.WriteLine("This solution is not FIFO. 26 comes before 21");

		var cts = new CancellationTokenSource();
		var queue = new PriorityQueue<QueueItem, int>();

		queue.Enqueue( new QueueItem { Name = "21", PriorityTitle = "Normal"}, 2);
		queue.Enqueue(new QueueItem { Name = "02", PriorityTitle = "Platinum"}, 0);
		queue.Enqueue(new QueueItem { Name = "13", PriorityTitle = "Gold"}, 1);
		queue.Enqueue(new QueueItem { Name = "14", PriorityTitle = "Gold"}, 1);
		queue.Enqueue(new QueueItem { Name = "05", PriorityTitle = "Platinum" }, 0);
		queue.Enqueue(new QueueItem { Name = "26", PriorityTitle = "Normal"}, 2);

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