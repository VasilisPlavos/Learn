using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.BackgroundTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private MyFile MyFile = new() { Dir = Path.Combine(AppContext.BaseDirectory, "storage"), FileName = "file.txt", FilePath = Path.Combine(AppContext.BaseDirectory, "storage", "file.txt") };

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            var response = await CheckStatusAsync();
            if (response == "start")
            {

                // This is the background task
                Task.Run(async () =>
                {
                    await Task.Delay(10000);
                    await SaveFileAsync("Generated");
                });

                response = "work in progress";
                await SaveFileAsync(response);
            }

            return response;
        }

        private async Task SaveFileAsync(string context)
        {
            Directory.CreateDirectory(MyFile.Dir);
            await using var sw = new StreamWriter(MyFile.FilePath);
            await sw.WriteAsync(context);
        }

        private async Task<string> CheckStatusAsync()
        {
            string context;
            if (System.IO.File.Exists(MyFile.FilePath))
            {
                using var sr = new StreamReader(MyFile.FilePath);
                context = await sr.ReadToEndAsync();
            }
            else
            {
                context = "start";
                await SaveFileAsync(context);
            }

            return context;
        }
    }

    internal class MyFile
    {
        public string Dir { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
