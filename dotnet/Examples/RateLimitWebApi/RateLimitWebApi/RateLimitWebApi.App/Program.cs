using AspNetCoreRateLimit;
using RateLimitWebApi.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// explanation: https://dotnetthoughts.net/implement-rate-limiting-in-asp-net-core-web-api/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

// builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting();

// IP Rate Limit configuration from appsettings.json.
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.EnableEndpointRateLimiting = true;
//    options.StackBlockedRequests = false;
//    options.HttpStatusCode = 429;
//    options.RealIpHeader = "X-Real-IP";
//    options.GeneralRules = new List<RateLimitRule>
//    {
//        new()
//        {
//            Endpoint = "*",
//            Period = "10s",
//            Limit = 2
//        }
//    };
//});

// Client Rate Limit configuration
builder.Services.Configure<ClientRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Period = "10s",
            Limit = 2
        }
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// this middleware activated once the request is blocked by the client rate limiter
// it works only for ClientRateLimitOptions not for IpRateLimitOptions
app.UseMiddleware<MyCustomClientRateLimitMiddleware>();

// To Implement middleware for IpRateLimitOptions:
// 1. I need a new Middleware class -> public class MyCustomClientRateLimitMiddleware : IpRateLimitMiddleware {}
// 2. I need to remove ->  app.UseIpRateLimiting();
app.UseIpRateLimiting();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
