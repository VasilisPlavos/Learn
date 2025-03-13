using Examples.y23.ImageSharp.Services;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.ImageSharp.ConsoleApp;

public class ConsoleApp6Service : IHostedService
{
    private readonly IImageSharpService _imageSharpService;

    public ConsoleApp6Service(IImageSharpService imageSharpService)
    {
        _imageSharpService = imageSharpService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _imageSharpService.RunAsync();
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}