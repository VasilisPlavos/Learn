using Contacts.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}
