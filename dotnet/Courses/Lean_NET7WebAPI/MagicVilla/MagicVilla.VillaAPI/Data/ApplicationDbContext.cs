using MagicVilla.VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.VillaAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		:base(options){}


		public DbSet<Villa> Villas { get; set; }
		public DbSet<VillaNumber> VillaNumbers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var villas = new List<Villa> { new() { Id = 1, Name = "Pool View 1", DateCreated = DateTime.UtcNow }, new() { Id = 2, Name = "Pool View 2" } };
			modelBuilder.Entity<Villa>().HasData(villas);
		}
	}
}
