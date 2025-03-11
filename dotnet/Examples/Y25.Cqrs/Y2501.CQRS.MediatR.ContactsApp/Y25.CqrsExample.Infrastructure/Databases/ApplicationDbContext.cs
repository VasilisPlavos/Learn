using Microsoft.EntityFrameworkCore;
using Y25.CqrsExample.Domain.Entities;

namespace Y25.CqrsExample.Infrastructure.Databases;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }
}