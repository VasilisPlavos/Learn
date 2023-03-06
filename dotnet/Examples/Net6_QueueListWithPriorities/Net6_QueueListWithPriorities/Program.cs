// source: https://www.youtube.com/watch?v=4XSSC6uPFNA

using Net6_QueueListWithPriorities;

await ExampleWithSeperatedQueues.RunAsync();
await ExampleWithSimplePriorityQueue.RunAsync();
await ExampleWithPriorityQueuesAndItemsInQueues.RunAsync();
await ExampleWithPriorityQueuesAndItemsInQueuesBetterApproach.RunAsync();

Console.WriteLine("done");