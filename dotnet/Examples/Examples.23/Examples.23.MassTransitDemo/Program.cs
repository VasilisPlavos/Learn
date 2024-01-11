using Examples._23.MassTransitDemo.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHostedService<PingPublisher>();

var app = builder.Build();
app.Run();

public record Ping(string Button);