using System.Net.Http.Json;
using Y25.ManyProcessors.Dtos.Flatfox;

namespace Y25.ManyProcessors.Processors;

public class FlatfoxService
{
    private readonly HttpClient _client = new(
        new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1) // We are doing this because if DNS will change, out HttpClient will stop working
        });

    public async Task<List<PinDto>?> GetPinsAsync(GetPinsRequestDto request)
    {
        var pinsResponse = await _client.GetAsync($"https://flatfox.ch/api/v1/pin/?east={request.East}&north={request.North}&south={request.South}&west={request.West}&max_count={request.MaxCount}");
        if (pinsResponse.IsSuccessStatusCode) return await pinsResponse.Content.ReadFromJsonAsync<List<PinDto>>();

        var responseString = await pinsResponse.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        throw new Exception($"Error while getting pins from Flatfox API. Status code: {pinsResponse.StatusCode}. Response: {responseString}.");
    }
}