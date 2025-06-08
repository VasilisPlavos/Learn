using Examples.y24.Common.Dtos.Joke;

namespace Examples.y24.HttpClientExamples.Services;

public interface IJokesService
{
    Task<JokesResults> GetJokeBySearchQueryAsync(string searchQuery);
    Task<Joke> GetRandomJoke1Async();
    Task<Joke> GetRandomJoke2Async();
    Task<Joke> GetRandomJoke3Async();
}

public class JokesService : IJokesService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client = new(
        new SocketsHttpHandler
        {
            // We are doing this because if DNS will change, out HttpClient will stop working
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        });

    public JokesService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<JokesResults> GetJokeBySearchQueryAsync(string searchQuery)
    {
        var response = await _client.GetAsync($"https://api.chucknorris.io/jokes/search?query={searchQuery}");
        return await response.Content.ReadFromJsonAsync<JokesResults>() ?? new JokesResults();
    }


    // worst implementation - 300 ms
    public async Task<Joke> GetRandomJoke1Async()
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://api.chucknorris.io/jokes/random");
        return await response.Content.ReadFromJsonAsync<Joke>() ?? new Joke();
    }


    // not bad implementation but still not recommended according to Nick Chapsas - 100 ms
    public async Task<Joke> GetRandomJoke2Async()
    {
        var response = await _client.GetAsync($"https://api.chucknorris.io/jokes/random");
        return await response.Content.ReadFromJsonAsync<Joke>() ?? new Joke();
    }

    // recommended implementation according to Nick Chapsas - 100 ms
    // configured in Program.cs:line11
    public async Task<Joke> GetRandomJoke3Async()
    {
        var client = _httpClientFactory.CreateClient("JokesApi");
        return await client.GetFromJsonAsync<Joke>("random") ?? new Joke();
    }
}