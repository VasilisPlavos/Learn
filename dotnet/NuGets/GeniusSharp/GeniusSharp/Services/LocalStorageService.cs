using GeniusSharp.Dtos;
using GeniusSharp.GeniusApiResponseDtos;
using Newtonsoft.Json;

namespace GeniusSharp.Services;

internal static class LocalStorageService
{
    public static async Task<Artist?> GetArtistAsync(string artistName)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "storage", "artists", artistName, $"{artistName}.json");
        try
        {
            using var sr = new StreamReader(filePath);
            var json = await sr.ReadToEndAsync();
            var artist = JsonConvert.DeserializeObject<Artist>(json);
            return artist;
        }
        catch
        {
            // ignored
        }

        return null;
    }

    public static async Task<List<SongsResponseDto.Song>?> GetSongsAsync(int artistGeniusId)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "storage", "artistIds", $"{artistGeniusId}", "songs.json");
        try
        {
            using var sr = new StreamReader(filePath);
            var json = await sr.ReadToEndAsync();
            var songs = JsonConvert.DeserializeObject<List<SongsResponseDto.Song>>(json);
            return songs;
        }
        catch
        {
            // ignored
        }

        return null;
    }


    public static async Task SaveArtistAsync(Artist artist)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "storage", "artists", artist.Name);
        Directory.CreateDirectory(directory);

        var json = JsonConvert.SerializeObject(artist);
        var filePath = Path.Combine(directory, $"{artist.Name}.json");
        await using var sw = new StreamWriter(filePath);
        await sw.WriteAsync(json);
    }

    public static async Task SaveSongsAsync(int artistGeniusId, List<SongsResponseDto.Song> songs)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "storage", "artistIds", $"{artistGeniusId}");
        Directory.CreateDirectory(directory);

        var json = JsonConvert.SerializeObject(songs);
        var filePath = Path.Combine(directory, "songs.json");
        await using var sw = new StreamWriter(filePath);
        await sw.WriteAsync(json);
    }
}