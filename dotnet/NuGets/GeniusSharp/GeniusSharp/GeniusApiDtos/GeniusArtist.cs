namespace GeniusSharp.GeniusApiDtos;

public record GeniusArtist
{
    public string api_path { get; set; }
    public string header_image_url { get; set; }
    public int id { get; set; }
    public string image_url { get; set; }
    public bool is_meme_verified { get; set; }
    public bool is_verified { get; set; }
    public string name { get; set; }
    public string url { get; set; }
}