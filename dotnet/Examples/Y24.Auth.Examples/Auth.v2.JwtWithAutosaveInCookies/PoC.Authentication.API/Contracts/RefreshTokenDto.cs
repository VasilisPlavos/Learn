using System.ComponentModel.DataAnnotations;

namespace PoC.Authentication.API.Contracts;

public class RefreshTokenDto
{
    [Required]
    public string Value { get; set; }
}