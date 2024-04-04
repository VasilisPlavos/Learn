namespace Examples.y24.HttpClientExamples.Dtos.Pushbullet;

public class push
{
    public bool active { get; set; }
    public string body { get; set; }
    public float created { get; set; }
    public string direction { get; set; }
    public bool dismissed { get; set; }
    public string iden { get; set; }
    public float modified { get; set; }
    public string receiver_email { get; set; }
    public string receiver_email_normalized { get; set; }
    public string receiver_iden { get; set; }
    public string sender_email { get; set; }
    public string sender_email_normalized { get; set; }
    public string sender_iden { get; set; }
    public string sender_name { get; set; }
    public string title { get; set; }
    public string type { get; set; }
}