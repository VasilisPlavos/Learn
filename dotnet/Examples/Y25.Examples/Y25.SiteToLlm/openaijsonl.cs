namespace Y25.SiteToLlm;

public class openaijsonl
{

    public Message[] messages { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}