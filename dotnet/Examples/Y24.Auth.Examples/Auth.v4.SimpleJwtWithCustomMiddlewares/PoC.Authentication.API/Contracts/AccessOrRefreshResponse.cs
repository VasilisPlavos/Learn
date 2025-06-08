namespace PoC.Authentication.API.Contracts;

public class AccessOrRefreshResponse
{
    public string access_token { get; set; }
    public long access_token_exp { get; set; }
    public string refresh_token { get; set; }
    public long refresh_token_exp { get; set; }
}