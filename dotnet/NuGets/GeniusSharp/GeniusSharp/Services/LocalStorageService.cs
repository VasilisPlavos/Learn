using GeniusSharp.Dtos;
using GeniusSharp.GeniusApiDtos;
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
        try
        {
            var directory = Path.Combine(AppContext.BaseDirectory, "storage", "artists", artist.Name);
            Directory.CreateDirectory(directory);
            var filePath = Path.Combine(directory, $"{artist.Name}.json");
            if (File.Exists(filePath)) return;

            var json = JsonConvert.SerializeObject(artist);
            await using var sw = new StreamWriter(filePath);
            await sw.WriteAsync(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static async Task SaveArtistsAsync(List<SongsResponseDto.Song>? songs)
    {
        if (songs == null) return;

        var taskList = new List<Task>();
        foreach (var song in songs)
        {
            foreach (var artist in song.featured_artists)
            {
                taskList.Add(SaveArtistAsync(new Artist { Name = artist.name, GeniusId = artist.id, GeniusProfilePicture = artist.image_url, IsGeniusVerified = artist.is_verified }));
            }

            foreach (var artist in song.primary_artists)
            {
                taskList.Add(SaveArtistAsync(new Artist { Name = artist.name, GeniusId = artist.id, GeniusProfilePicture = artist.image_url, IsGeniusVerified = artist.is_verified }));
            }
        }

        await Task.WhenAll(taskList);
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