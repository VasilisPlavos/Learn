using System.ComponentModel.DataAnnotations;

namespace Y25.Database.Entities;

public class Contact
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}