using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Examples._23.InParallel;

public interface IInParallelService
{
    Task<bool> RunAsync();
}

public class InParallelService : IInParallelService
{
    private static readonly HttpClient HttpClient = new();
    private const int TaskCount = 1000;
    private const string RegExPattern = "value\":\"([^\"]+)\"";

    public async Task<bool> RunAsync()
    {
        var tasksList = await ForEachVersionAsync();
        var tasksList2 = await ForEachVersion2Async();
        var parallelList = ParallelVersion();
        var parallelList2 = ParallelVersionByFiratAsync();
        var parallelList3 = ParallelVersionWithOptions();
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

        // Create a Regex object and match the pattern
        var match = Regex.Match(response, RegExPattern);
        if (!match.Success) return response;

        // The desired text is in the first capture group
        var result = match.Groups[1].Value;
        return result;
    }
    private IReadOnlyCollection<string> ParallelVersion()
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

        await Parallel.ForEachAsync(tasksList, parallelOptions: options, body: async (strTask, _) =>
        {
            list.Add(await strTask);
        });

        return list;
    }


    // Important -> Use IReadOnlyCollection and not List 
    private IReadOnlyCollection<string> ParallelVersionWithOptions()
    {
        var list = new ConcurrentBag<string>();
        var tasksList = Enumerable.Range(0, TaskCount)
            .Select(_ => new Func<string>(() => GetJokeAsync(HttpClient).GetAwaiter().GetResult()))
            .ToList();

        Parallel.For(0, tasksList.Count,
            new ParallelOptions { MaxDegreeOfParallelism = 4 },
            i => list.Add(tasksList[i]()));
        return list;
    }
}