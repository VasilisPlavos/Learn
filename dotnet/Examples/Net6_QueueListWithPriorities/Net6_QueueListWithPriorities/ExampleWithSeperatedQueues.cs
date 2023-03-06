namespace Net6_QueueListWithPriorities;

public static class ExampleWithSeperatedQueues
{
	public static async Task RunAsync()
	{
		var cts = new CancellationTokenSource();

		var normalQueue = new Queue<string>();
		var goldQueue = new Queue<string>();
		var platinumQueue = new Queue<string>();

		normalQueue.Enqueue("31");
		platinumQueue.Enqueue("11");
		goldQueue.Enqueue("21");
		goldQueue.Enqueue("22");
		platinumQueue.Enqueue("12");
		normalQueue.Enqueue("32");

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