using System.ComponentModel.DataAnnotations;

namespace PoC.Authentication.API.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string? Email { get; set; }
    [Required]
    public string Role { get; set; }
}