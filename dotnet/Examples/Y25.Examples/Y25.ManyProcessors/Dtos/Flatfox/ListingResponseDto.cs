namespace Y25.ManyProcessors.Dtos.Flatfox;

public class ListingResponseDto
{
    public int pk { get; set; }
    public string slug { get; set; }
    public string url { get; set; }
    public string short_url { get; set; }
    public string submit_url { get; set; }
    public string status { get; set; }
    public DateTime created { get; set; }
    public string offer_type { get; set; }
    public string object_category { get; set; }
    public string object_type { get; set; }
    public string reference { get; set; }
    public string ref_property { get; set; }
    public string ref_house { get; set; }
    public string ref_object { get; set; }
    public string alternative_reference { get; set; }
    public int? price_display { get; set; }
    public string price_display_type { get; set; }
    public string price_unit { get; set; }
    public DateTime published { get; set; }
    public object rent_net { get; set; }
    public object rent_charges { get; set; }
    public int? rent_gross { get; set; }
    public string short_title { get; set; }
    public string public_title { get; set; }
    public string pitch_title { get; set; }
    public string description_title { get; set; }
    public string description { get; set; }
    public int? surface_living { get; set; }
    public object surface_property { get; set; }
    public object surface_usable { get; set; }
    public object surface_usable_minimum { get; set; }
    public object volume { get; set; }
    public int? space_display { get; set; }
    public string number_of_rooms { get; set; }
    public int? floor { get; set; }
    public Attribute[] attributes { get; set; }
    public bool is_furnished { get; set; }
    public bool is_temporary { get; set; }
    public bool is_selling_furniture { get; set; }
    public object street { get; set; }
    public int? zipcode { get; set; }
    public string city { get; set; }
    public string public_address { get; set; }
    public float latitude { get; set; }
    public float longitude { get; set; }
    public object year_built { get; set; }
    public object year_renovated { get; set; }
    public string moving_date_type { get; set; }
    public object moving_date { get; set; }
    public string video_url { get; set; }
    public string tour_url { get; set; }
    public string website_url { get; set; }
    public string live_viewing_url { get; set; }
    public Cover_Image cover_image { get; set; }
    public int[] images { get; set; }
    public object[] documents { get; set; }
    public Agency agency { get; set; }
    public bool is_liked { get; set; }
    public bool is_disliked { get; set; }
    public bool is_subscribed { get; set; }
    public bool reserved { get; set; }
    public object state { get; set; }
    public string country { get; set; }
    public string smg_id { get; set; }
    public string rent_title { get; set; }
    public int? livingspace { get; set; }
}

public class Cover_Image
{
    public int pk { get; set; }
    public string caption { get; set; }
    public string url { get; set; }
    public string url_thumb_m { get; set; }
    public string url_listing_search { get; set; }
    public int? ordering { get; set; }
    public int? width { get; set; }
    public int? height { get; set; }
}

public class Agency
{
    public string name { get; set; }
    public string name_2 { get; set; }
    public string street { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public Logo logo { get; set; }
}

public class Logo
{
    public string url { get; set; }
    public string url_org_logo_m { get; set; }
}

public class Attribute
{
    public string name { get; set; }
}


