using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_gql_test.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_gql_test.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options){}

        public DbSet<Locations> Location { get; set; }

    }
}
