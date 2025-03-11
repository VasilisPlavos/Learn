using System.ComponentModel.DataAnnotations;

namespace PoC.Authentication.API.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
}