using Examples.y24.Net8Auth.Api;
using Examples.y24.Net8Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Sources:
// https://youtu.be/sZnu-TyaGNk
// https://devblogs.microsoft.com/dotnet/whats-new-with-identity-in-dotnet-8/

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add and configure Auth
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

// Register the DbContext on the service container
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    // must use a real database here to store the users
    options.UseInMemoryDatabase("InMemoryDb");
});

builder.Services.AddIdentityCore<MyUser>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddApiEndpoints();

// const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: myAllowSpecificOrigins, configurePolicy: configurePolicy =>
//         {
//             configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//         });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.UseCors(myAllowSpecificOrigins);

app.MapControllers();
app.MapIdentityApi<MyUser>();

app.Run();
