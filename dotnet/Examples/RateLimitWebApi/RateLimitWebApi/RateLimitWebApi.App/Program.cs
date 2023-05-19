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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
