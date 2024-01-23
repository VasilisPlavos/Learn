using Examples._23.ImageSharp.Services;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.ImageSharp.ConsoleApp;

public class ConsoleAppService : IHostedService
{
    private readonly IImageSharpService _imageSharpService;

    public ConsoleAppService(IImageSharpService imageSharpService)
    {
        _imageSharpService = imageSharpService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Cleaner is running...");
        await _imageSharpService.RunAsync();
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}