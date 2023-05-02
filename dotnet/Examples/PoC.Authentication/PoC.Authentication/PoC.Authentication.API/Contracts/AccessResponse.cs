namespace PoC.Authentication.API.Contracts;

public class AccessOrRefreshResponse
{
    public string AccessToken { get; set; }
    public string AccessToken_Exp { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshToken_Exp { get; set; }
}