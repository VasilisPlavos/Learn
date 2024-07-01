using System.Net.Http.Json;
using GeniusSharp.Dtos;
using GeniusSharp.GeniusApiDtos;
using GeniusSharp.Services;

namespace GeniusSharp;

// TODO: https://github.com/microsoft/playwright-dotnet

public class Genius(string accessToken)
{
    private readonly HttpClient _client = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(1) // We are doing this because if DNS will change, out HttpClient will stop working
    });


    public async Task<List<SongsResponseDto.Song>> SearchSongsByArtistAsync(string artistName, int maxSongs = 50, string sort = "popularity", bool includeFeatures=false)
    {
        var artist = await SearchArtistAsync(artistName);
        if (artist == null) throw new NotImplementedException();

        return await GetSongsAsync(artist.GeniusId, maxSongs, sort, includeFeatures);
    }

    public async Task<List<SongsResponseDto.Song>> GetSongsAsync(int artistGeniusId, int maxSongs = 50, string sort = "popularity", bool includeFeatures = false)
    {
        var songs = await LocalStorageService.GetSongsAsync(artistGeniusId);
        if (songs != null)
        {
            await LocalStorageService.SaveArtistsAsync(songs);
            return includeFeatures ? songs : songs.Where(s => s.primary_artists.Any(a => a.id == artistGeniusId)).ToList();
        }

        songs = new List<SongsResponseDto.Song>();

        int? page = 1;
        while (page != null)
        {
            var response = await _client.GetAsync($"http://api.genius.com/artists/{artistGeniusId}/songs?access_token={accessToken}&per_page={maxSongs}&page={page}&sort={sort}");
            var responseDto = await response.Content.ReadFromJsonAsync<SongsResponseDto.Rootobject>();

            if (responseDto?.meta.status != 200) throw new NotImplementedException();

            var relatedSongs = responseDto.response.songs.Where(s => s.featured_artists.Any(fa => fa.id == artistGeniusId) || s.primary_artists.Any(pr => pr.id == artistGeniusId)).ToList();
            songs.AddRange(relatedSongs);

            page = responseDto.response.next_page;
        }

        await LocalStorageService.SaveSongsAsync(artistGeniusId, songs);
        return includeFeatures ? songs : songs.Where(s => s.primary_artists.Any(a => a.id == artistGeniusId)).ToList();
    }

    // https://genius.com/discussions/280987-Whats-the-easiest-way-to-get-an-artist-id
    public async Task<int?> SearchArtistIdAsync(string artistName)
    {
        var artist = await SearchArtistAsync(artistName);
        return artist?.GeniusId;
    }

    public async Task<Artist?> SearchArtistAsync(string artistName)
    {
        var artist = await LocalStorageService.GetArtistAsync(artistName);
        if (artist != null) return artist;

        var response = await SearchAsync(artistName);
        if (response == null) return null;

        var hits = response.response.hits.Where(x => x.result.primary_artists.Length == 1).ToList();
        foreach (var hit in hits)
        {
            var hitArtist = hit.result.primary_artists.FirstOrDefault();
            if (hitArtist?.name != artistName) continue;

            artist = new Artist
            {
                GeniusId = hitArtist.id,
                IsGeniusVerified = hitArtist.is_verified,
                Name = hitArtist.name,
                GeniusProfilePicture = hitArtist.image_url
            };

            await LocalStorageService.SaveArtistAsync(artist);
            return artist;
        }

        if (response.response.hits.Length == 0) return null;
        throw new NotImplementedException();
    }

    private async Task<SearchResponseDto.Rootobject?> SearchAsync(string artistName)
    {
        var response = await _client.GetAsync($"http://api.genius.com/search?q={artistName}&access_token={accessToken}&per_page=50");
        // var responseString = await response.Content.ReadAsStringAsync();
        // var responseDto = JsonConvert.DeserializeObject<SearchResponseDto.Rootobject>(responseString);
        var responseDto = await response.Content.ReadFromJsonAsync<SearchResponseDto.Rootobject>();

        if (responseDto?.meta.status != 200) throw new NotImplementedException();
        return responseDto;
    }
}
