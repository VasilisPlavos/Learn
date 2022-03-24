using Example.Cloudon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Cloudon.API.Databases
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
