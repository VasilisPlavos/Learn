using Examples._23.ImageSharp.Services;
using Examples.y24.Azure.StorageAccount.Services;
using Microsoft.Extensions.Hosting;

namespace Examples.y24.ImageSharp.ConsoleApp;

public class ConsoleAppService : IHostedService
{
    private readonly IImageSharpService _imageSharpService;
    private readonly IStorageAccountService _storageAccountService;

    public ConsoleAppService(IImageSharpService imageSharpService, IStorageAccountService storageAccountService)
    {
        _imageSharpService = imageSharpService;
        _storageAccountService = storageAccountService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _storageAccountService.RunAsync();
        await _imageSharpService.RunAsync();
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}