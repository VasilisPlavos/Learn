using System.Net.Http.Headers;
using Examples.y24.Common.Dtos.Pushbullet;
using Microsoft.AspNetCore.Mvc;

namespace Examples.y24.HttpClientExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushbulletController : ControllerBase
    {
        private readonly HttpClient _client = new();

        [HttpPost]
        public async Task<IActionResult> DeleteAllPushbulletAsync()
        {
            var apiKey = "";
            if (string.IsNullOrEmpty(apiKey)) return BadRequest("Add Pushbullet API key manually");

            _client.DefaultRequestHeaders.Add("Access-Token", apiKey);
            var responseMessage = await ProceedAsync();
            return Ok(responseMessage);
        }

        private async Task<string> ProceedAsync()
        {
            var cursor = "";
            while (true)
            {
                var response = await _client.GetAsync($"https://api.pushbullet.com/v2/pushes?cursor={cursor}&active=true");

                var remaining = GetRemaining(response.Headers);
                if (remaining is null) return "Rate limit header not found";
                if (remaining < 100) return "Rate limit reached";

                var pushDto = await response.Content.ReadFromJsonAsync<PushDto>();
                var pushesToDelete = pushDto?.pushes?.Where(push => push.active).ToList();
                if (pushesToDelete == null || pushesToDelete.Count == 0) return "No pushes to delete";

                var taskList = new List<Task>();
                foreach (var push in pushesToDelete)
                {
                    taskList.Add(_client.DeleteAsync($"https://api.pushbullet.com/v2/pushes/{push.iden}"));
                }

                await Task.WhenAll(taskList);
                cursor = pushDto?.cursor ?? "";
            }
        }

        private static int? GetRemaining(HttpResponseHeaders responseHeaders)
        {
            var headers = responseHeaders.ToDictionary(l => l.Key, k => k.Value).ToList();
            var remaining = headers.FirstOrDefault(x => x.Key == "X-RateLimit-Remaining").Value.FirstOrDefault();
            return int.TryParse(remaining, out var result) ? result : null;
        }
    }
}
