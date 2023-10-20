using Examples._23.InParallel.Dtos;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Examples._23.InParallel;

public interface IInParallelService
{
    Task<bool> RunAsync();
}

public class InParallelService : IInParallelService
{
    private static readonly HttpClient HttpClient = new();
    private const int TaskCount = 1000;

    public async Task<bool> RunAsync()
    {
        // ParallelVersionWithOptions took 00:00:12.6815484
        var start = DateTime.UtcNow;
        var parallelList3 = ParallelVersionWithOptions();
        Console.WriteLine($"{nameof(ParallelVersionWithOptions)} took {(DateTime.UtcNow - start).TotalSeconds} sec");

        // parallelList2 took 00:00:04.4084283
        start = DateTime.UtcNow;
        var parallelList2 = await ParallelVersionByFiratAsync();
        Console.WriteLine($"{nameof(ParallelVersionByFiratAsync)} took {(DateTime.UtcNow - start).TotalSeconds} sec");

        // parallelList took 00:00:05.6568826
        start = DateTime.UtcNow;
        var parallelList = ParallelVersion();
        Console.WriteLine($"{nameof(ParallelVersion)} took {(DateTime.UtcNow - start).TotalSeconds} sec");

        // tasksList2 took 00:00:05.4613256
        start = DateTime.UtcNow;
        var tasksList2 = await ForEachVersion2Async();
        Console.WriteLine($"{nameof(ForEachVersion2Async)} took {(DateTime.UtcNow - start).TotalSeconds} sec");

        // tasksList took 00:01:47.5823954
        start = DateTime.UtcNow;
        var tasksList = await ForEachVersionAsync();
        Console.WriteLine($"{nameof(ForEachVersionAsync)} took {(DateTime.UtcNow - start).TotalSeconds} sec");
        return true;
    }

    private static async Task<List<string>> ForEachVersionAsync()
    {
        var list = new List<string>();

        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => new Func<Task<string>>(() => GetJokeAsync(HttpClient)))
            .ToList();

        foreach (var task in tasksList) list.Add(await task());
        return list;
    }

    private static async Task<List<string>> ForEachVersion2Async()
    {
        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => GetJokeAsync(HttpClient));

        var results = await Task.WhenAll(tasksList);
        return results.ToList();
    }

    private static async Task<string> GetJokeAsync(HttpClient httpClient)
    {
        // fake api results
        //return $"{DateTime.UtcNow.Ticks}";

        var response = await httpClient.GetStringAsync(new Uri("https://api.chucknorris.io/jokes/random"));
        return JsonSerializer.Deserialize<ChuckNorrisResponse>(response)!.value; ;
    }
    private IEnumerable<string> ParallelVersion()
    {
        var list = new ConcurrentBag<string>();

        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => new Func<string>(() => GetJokeAsync(HttpClient).GetAwaiter().GetResult()))
            .ToList();

        Parallel.For(0, tasksList.Count, i => list.Add(tasksList[i]()));
        return list;
    }

    private async Task<IReadOnlyCollection<string>> ParallelVersionByFiratAsync()
    {
        var list = new ConcurrentBag<string>();

        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => GetJokeAsync(HttpClient))
            .ToList();

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount,
            CancellationToken = CancellationToken.None
        };

        await Parallel.ForEachAsync(tasksList, parallelOptions: options, body: async (strTask, _) => { list.Add(await strTask); });
        return list;
    }


    // Important -> Use IReadOnlyCollection and not List 
    private IReadOnlyCollection<string> ParallelVersionWithOptions()
    {
        var list = new ConcurrentBag<string>();

        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => new Func<string>(() => GetJokeAsync(HttpClient).GetAwaiter().GetResult()))
            .ToList();

        Parallel.For(0, tasksList.Count, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, i => list.Add(tasksList[i]()));
        return list;
    }
}