namespace Net6_QueueListWithPriorities;

public static class ExampleWithSeperatedQueues
{
	public static async Task RunAsync()
	{
		var cts = new CancellationTokenSource();

		var normalQueue = new Queue<string>();
		var goldQueue = new Queue<string>();
		var platinumQueue = new Queue<string>();

		normalQueue.Enqueue("21");
		platinumQueue.Enqueue("02");
		goldQueue.Enqueue("13");
		goldQueue.Enqueue("14");
		platinumQueue.Enqueue("05");
		normalQueue.Enqueue("26");

		while (!cts.IsCancellationRequested)
		{
			await Task.Delay(1000, cts.Token);
			if (platinumQueue.Any())
			{
				var x = platinumQueue.Dequeue();
				Console.WriteLine($"platinum: {x}");
				continue;
			}

			if (goldQueue.Any())
			{
				var x = goldQueue.Dequeue();
				Console.WriteLine($"gold: {x}");
				continue;
			}

			if (normalQueue.Any())
			{
				var x = normalQueue.Dequeue();
				Console.WriteLine($"normal: {x}");
				continue;
			}


			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			cts.Cancel();
		}
	}
}