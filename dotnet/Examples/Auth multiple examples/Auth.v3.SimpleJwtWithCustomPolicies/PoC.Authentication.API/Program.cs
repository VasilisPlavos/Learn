using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

using PoC.Authentication.API.Data;
using PoC.Authentication.API.Services;
using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorize users using Json Web Value. Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});

var bearerSettings = new BearerSettings();
builder.Configuration.Bind("JWT", bearerSettings);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddSingleton<IAuthorizationHandler, SessionHandler>();
builder.Services.AddSingleton(bearerSettings);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
            ValidateIssuer = false, // TODO: on production make it true
            ValidateIssuerSigningKey = true,
            ValidateAudience = false, // TODO: on production make it true
            ValidateLifetime = true,
            ValidAudiences = bearerSettings.ValidAudiences,
            ValidIssuers = bearerSettings.ValidIssuers,
            ClockSkew = TimeSpan.FromSeconds(10),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SessionPolicy", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("X-Session-Id"));
    });
});

//builder.Services.AddCors(opts => opts.AddPolicy(name: "NgOrigins", policy =>
//{
//    policy.WithOrigins(builder.Configuration["JWT:Audiences"].Trim().Split(',')).AllowAnyMethod().AllowAnyHeader();
//}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
