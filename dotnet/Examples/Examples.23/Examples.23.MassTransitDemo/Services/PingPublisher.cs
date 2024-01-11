namespace Examples._23.MassTransitDemo.Services;

public class PingPublisher : BackgroundService
{
    private readonly ILogger<PingPublisher> _logger;

    public PingPublisher(ILogger<PingPublisher> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();

            var keyPressed = Console.ReadKey(true);
            if (keyPressed.Key != ConsoleKey.Escape)
            {
                _logger.LogInformation($"Pressed {keyPressed.Key.ToString()}");
            }

            await Task.Delay(200);
        }
    }
}