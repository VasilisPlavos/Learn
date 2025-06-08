using Examples.y23.MassTransitDemo.Services;
using MassTransit;

// https://www.youtube.com/watch?v=4FFYefcx4Bg

var builder = WebApplication.CreateBuilder();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<PingPublisher>();

var app = builder.Build();
app.Run();