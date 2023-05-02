namespace PoC.Authentication.API.Contracts;

public class RegisterRequest
{
    public string? Email { get; set; }
    public string? JwtToMerge { get; set; }
    public string? Password { get; set; }
}