﻿using System.Net.Http.Json;
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
        try
        {
            return await pinsResponse.Content.ReadFromJsonAsync<List<PinDto>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var responseString = await pinsResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            throw new Exception($"Error while getting pins from Flatfox API. Status code: {pinsResponse.StatusCode}. Response: {responseString}.");
        }
    }

    // This method is used to get listings by ids. It is used in the FlatfoxService class.
    // The limit of ids is around 450
    public async Task<List<ListingResponseDto>?> GetListingsAsync(List<int> ids)
    {
        var idsString = string.Join("&pk=", ids);
        var url = $"https://flatfox.ch/api/v1/public-listing/?expand=cover_image&include=is_liked&include=is_disliked&include=is_subscribed&limit=0&pk={idsString}";
        var response = await _client.GetAsync(url);
        try
        {
            return await response.Content.ReadFromJsonAsync<List<ListingResponseDto>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            throw new Exception($"Error while getting pins from Flatfox API. Status code: {response.StatusCode}. Response: {responseString}.");
        }
    }




}