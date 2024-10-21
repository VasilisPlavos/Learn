using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.BackgroundTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MyFile _file = new() { Dir = Path.Combine(AppContext.BaseDirectory, "storage"), FilePath = Path.Combine(AppContext.BaseDirectory, "storage", "file.txt") };

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            var status = await CheckStatusAsync();
            if (status == "start")
            {

                // Start background task
                _ = Task.Run(async () =>
                {
                    await Task.Delay(10000);  // Simulate background work
                    await SaveFileAsync("Generated");
                });

                status = "work in progress";
                await SaveFileAsync(status);
            }

            return status;
        }

        private async Task SaveFileAsync(string context)
        {
            Directory.CreateDirectory(_file.Dir!);
            await System.IO.File.WriteAllTextAsync(_file.FilePath!, context);
        }

        private async Task<string> CheckStatusAsync()
        {
            if (System.IO.File.Exists(_file.FilePath))
            {
                return await System.IO.File.ReadAllTextAsync(_file.FilePath);
            }

            const string initStatus = "start";
            await SaveFileAsync(initStatus);
            return initStatus;
        }
    }

    internal class MyFile
    {
        public string? Dir { get; set; }
        public string? FilePath { get; set; }
    }
}
