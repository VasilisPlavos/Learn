namespace GeniusSharp.GeniusApiDtos;

public class SongsResponseDto
{

    public class Rootobject
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class Response
    {
        public Song[] songs { get; set; }
        public int? next_page { get; set; }
    }

    public class Song
    {
        public int annotation_count { get; set; }
        public string api_path { get; set; }
        public string artist_names { get; set; }
        public string full_title { get; set; }
        public string header_image_thumbnail_url { get; set; }
        public string header_image_url { get; set; }
        public int id { get; set; }
        public int lyrics_owner_id { get; set; }
        public string lyrics_state { get; set; }
        public string path { get; set; }
        public string primary_artist_names { get; set; }
        public object pyongs_count { get; set; }
        public string relationships_index_url { get; set; }
        public object release_date_components { get; set; }
        public object release_date_for_display { get; set; }
        public object release_date_with_abbreviated_month_for_display { get; set; }
        public string song_art_image_thumbnail_url { get; set; }
        public string song_art_image_url { get; set; }
        public Stats stats { get; set; }
        public string title { get; set; }
        public string title_with_featured { get; set; }
        public string url { get; set; }
        public GeniusArtist[] featured_artists { get; set; }
        public GeniusArtist primary_artist { get; set; }
        public GeniusArtist[] primary_artists { get; set; }
    }

    public class Stats
    {
        public int unreviewed_annotations { get; set; }
        public bool hot { get; set; }
    }



}