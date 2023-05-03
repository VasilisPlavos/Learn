namespace PoC.Authentication.API.Contracts;

public class AccessRequest
{
    public bool Anonymous { get; set; }
    public string? Email { get; set; }
    public string? SourceJwtToMove { get; set; }
    public string? Password { get; set; }
}