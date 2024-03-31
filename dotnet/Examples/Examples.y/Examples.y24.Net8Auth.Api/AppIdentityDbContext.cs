using Examples.y24.Net8Auth.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examples.y24.Net8Auth.Api;

public class AppIdentityDbContext : IdentityDbContext<MyUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }
}