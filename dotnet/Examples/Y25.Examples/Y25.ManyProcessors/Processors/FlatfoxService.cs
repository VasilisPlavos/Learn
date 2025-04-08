using Bogus.Bson;
using Newtonsoft.Json;
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

    // Flatfox API has a limit of 1000 pins per request;
    public async Task<List<PinDto>?> GetPinsAsync(string east, string north, string south, string west, int maxCount = 1000)
    {
        var pinsResponse = await _client.GetAsync($"https://flatfox.ch/api/v1/pin/?east={east}&north={north}&south={south}&west={west}&max_count={maxCount}");
        if (pinsResponse.IsSuccessStatusCode) return await pinsResponse.Content.ReadFromJsonAsync<List<PinDto>>();

        var responseString = await pinsResponse.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        throw new Exception($"Error while getting pins from Flatfox API. Status code: {pinsResponse.StatusCode}. Response: {responseString}.");
    }
}