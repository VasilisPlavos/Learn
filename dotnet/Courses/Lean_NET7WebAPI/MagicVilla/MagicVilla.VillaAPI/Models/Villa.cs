using System.ComponentModel.DataAnnotations;

namespace MagicVilla.VillaAPI.Models;

public class Villa
{
	[Key]
	public  int Id { get; set; }
	public string Name { get; set; }
	public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}