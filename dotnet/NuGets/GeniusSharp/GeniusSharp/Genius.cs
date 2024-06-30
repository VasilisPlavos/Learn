using System.Net.Http.Json;
using GeniusSharp.Dtos;
using GeniusSharp.GeniusApiResponseDtos;

namespace GeniusSharp;

// TODO: https://github.com/microsoft/playwright-dotnet

public class Genius(string accessToken)
{
    private readonly HttpClient _client = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(1) // We are doing this because if DNS will change, out HttpClient will stop working
    });


    public async Task<List<Song>> SearchSongsByArtistAsync(string artistName, int? maxSongs = null, string sort = "popularity", bool includeFeatures=false)
    {
        var artistId = await SearchArtistIdAsync(artistName);
        throw new NotImplementedException();
    }

    // https://genius.com/discussions/280987-Whats-the-easiest-way-to-get-an-artist-id
    public async Task<int?> SearchArtistIdAsync(string artistName)
    {
        var response = await SearchAsync(artistName);
        if (response == null) return null;

        var hits = response.response.hits.ToList();

        foreach (var hit in hits.Where(x => x.result.primary_artists.Length == 1))
        {
            var hitArtist = hit.result.primary_artists.FirstOrDefault();
            if (hitArtist == null) continue;
            if (hitArtist.name == artistName)
            {
                var artist = new Artist
                {
                    GeniusId = hitArtist.id,
                    IsGeniusVerified = hitArtist.is_verified,
                    Name = hitArtist.name,
                    GeniusProfilePicture = hitArtist.image_url
                };

                return artist.GeniusId;
            }
        }
        throw new NotImplementedException();
    }

    private async Task<SearchResponseDto?> SearchAsync(string artistName)
    {
        var response = await _client.GetAsync($"http://api.genius.com/search?q={artistName}&access_token={accessToken}");
        var responseDto = await response.Content.ReadFromJsonAsync<SearchResponseDto>();
        
        if (responseDto?.meta.status != 200) throw new NotImplementedException();
        return responseDto;
    }
}
