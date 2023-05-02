using System.ComponentModel.DataAnnotations;

namespace PoC.Authentication.API.Entities;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 30 days to ask for a new one!
    /// If user will not ask for a new one in the next 30 days it expired.
    /// </summary>
    [Required]
    public DateTime ExpirationDate { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Value { get; set; }
}