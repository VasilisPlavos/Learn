using Microsoft.EntityFrameworkCore;
using Y25.Database;
using Y25.Database.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbServices(builder.Configuration);


// add-migration db1
// update-database
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection-Testing")));

var app = builder.Build();


app.MapGet("/", () => "Database project is running...");
app.Run();


