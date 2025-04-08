namespace Y25.ManyProcessors.Dtos.Flatfox;

public class PinDto
{
    public int pk { get; set; }
    public string smg_id { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double? price_display { get; set; }
    public string price_display_type { get; set; }
    public string price_unit { get; set; }
    public double? selling_price { get; set; }
    public bool is_in_region { get; set; }
    public bool is_liked { get; set; }
    public bool is_disliked { get; set; }
    public object like_status { get; set; }
}


