using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.BackgroundTasks.Controllers
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
                // Start background tasks
                _ = Task.Factory.StartNew(() => RunBackgroundTask("Task 1"), HttpContext.RequestAborted);
                Task.Factory.StartNew(() => { });
                Task.Run( async () =>
                {
                    await Task.Delay(10000);  // Simulate background work
                    await SaveFileAsync("Task 2");
                });
                
                RunBackgroundEvent("Task 3");

                status = "work in progress";
                await SaveFileAsync(status);
            }

            return status;
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

        // Start background task
        private async void RunBackgroundEvent(string text)
        {
            await Task.Delay(15000);  // Simulate background work
            await SaveFileAsync(text);
        }

        private async Task RunBackgroundTask(string text)
        {
            await Task.Delay(5000);  // Simulate background work
            await SaveFileAsync(text);
        }

        private async Task SaveFileAsync(string context)
        {
            Directory.CreateDirectory(_file.Dir!);
            await System.IO.File.WriteAllTextAsync(_file.FilePath!, context);
        }
    }

    internal class MyFile
    {
        public string? Dir { get; set; }
        public string? FilePath { get; set; }
    }
}
