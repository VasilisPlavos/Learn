using MassTransit;

namespace Examples._23.MassTransitDemo.Services;

public class PingPublisher : BackgroundService
{
    private readonly ILogger<PingPublisher> _logger;
    private readonly IBus _bus;

    public PingPublisher(ILogger<PingPublisher> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();

            var keyPressed = Console.ReadKey(true);
            if (keyPressed.Key != ConsoleKey.Escape)
            {
               // _logger.LogInformation($"Pressed {keyPressed.Key}");
                await _bus.Publish(new Ping(keyPressed.Key.ToString()), stoppingToken);
            }

            await Task.Delay(200);
        }
    }
}