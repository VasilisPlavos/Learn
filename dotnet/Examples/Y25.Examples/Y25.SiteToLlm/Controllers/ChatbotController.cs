using Examples.y24.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using BrainSharp.Xml;
using Newtonsoft.Json;

namespace Y25.SiteToLlm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly string _openAiApiKey = Secrets.Get("OpenAiApiKey");

        private readonly HttpClient _client = new HttpClient(
            new SocketsHttpHandler
            {
                // We are doing this because if DNS will change, out HttpClient will stop working
                PooledConnectionLifetime = TimeSpan.FromMinutes(1)
            });

        [HttpPost("create-chatbot")]
        public async Task<IActionResult> CreateChatbot([FromBody] string? sitemapXml = "https://plavos.com/sitemap.xml")
        {
            // Step 1: Parse the sitemap to get URLs
            var urls = await ParseSitemap(sitemapXml);
            if (urls == null) return Ok($"Empty xml: {sitemapXml}");

            var trainingData = await TrainData(urls);
            if (trainingData == null) return Ok("No data for training");

            // Step 3: Fine-tune ChatGPT with OpenAI API
            var fineTuneId = await FineTuneModelAsync(trainingData);

            return Ok(new
            {
                Message = "Chatbot created successfully!",
                FineTuneId = fineTuneId
            });
        }


        private async Task<string> FineTuneModelAsync(List<TrainingExample> trainingData)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.openai.com/v1/fine_tuning/jobs"))
            //    {
            //        request.Headers.TryAddWithoutValidation("Authorization", "Bearer $OPENAI_API_KEY");

            //        request.Content = new StringContent("{\n    \"training_file\": \"file-fdsfdsdfsd\",\n    \"model\": \"gpt-4o-mini\"\n  }");
            //        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            //        var response = await httpClient.SendAsync(request);
            //    }
            //}


            var fineTuneEndpoint = "https://api.openai.com/v1/fine_tuning/jobs";

            var requestData = new
            {
                training_file = await UploadTrainingData(trainingData),
                model = "gpt-3.5-turbo"
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, fineTuneEndpoint);
            request.Headers.Add("Authorization", $"Bearer {_openAiApiKey}");
            request.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return responseJson!.id;
        }

        private async Task<StringContent?> JsonlStringifyAsync(List<TrainingExample> trainingData)
        {
            StringContent? result = null;
            //var dirPath = Path.Combine(AppContext.BaseDirectory, Guid.NewGuid().ToString());
            try
            {

                // Directory.CreateDirectory(dirPath);
                // var fileName = $"{Guid.NewGuid()}.jsonl";
                //var filePath = Path.Combine(dirPath, fileName);

                //await using var writer = System.IO.File.CreateText(filePath);

                //var sb = new StringBuilder(System.Text.Json.JsonSerializer.Serialize(trainingData[0]));
                //for (int i = 1; i < trainingData.Count; i++)
                //{
                //    sb.a
                //}

                var sb = new StringBuilder();
                foreach (var row in trainingData)
                {
                    sb.AppendLine(System.Text.Json.JsonSerializer.Serialize(row));
                    // await writer.WriteLineAsync(System.Text.Json.JsonSerializer.Serialize(row));
                }

                result = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //  Directory.Delete(dirPath, true);
            }

            var sss = await result.ReadAsStringAsync();
            return result;
        }

        private async Task<string> UploadTrainingData(List<TrainingExample> trainingData)
        {
            var uploadEndpoint = "https://api.openai.com/v1/files";

            // var trainingJson = JsonConvert.SerializeObject(trainingData);
            var jsonl = await JsonlStringifyAsync(trainingData);
            using var requestContent = new MultipartFormDataContent
            {
                { new StringContent("fine-tune"), "purpose" },
                { jsonl! }
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, uploadEndpoint)
            {
                Headers = { { "Authorization", $"Bearer {_openAiApiKey}" } },
                Content = requestContent
            };

            var response = await _client.SendAsync(request);
            // response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return responseJson!.id;
        }

        private async Task<List<TrainingExample>?> TrainData(List<string> urls)
        {
            var tasks = Enumerable.Range(0, urls.Count).Select(x => FetchPageContentAsync(urls[x]));
            var trainingData = await Task.WhenAll(tasks);
            return trainingData.Where(x => x?.Completion != null).Select(x => x).ToList()!;
        }

        private async Task<TrainingExample?> FetchPageContentAsync(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return new TrainingExample
                {
                    Completion = await response.Content.ReadAsStringAsync(),
                    Prompt = $"What is the content of the page at {url}?"
                };
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<string>?> ParseSitemap(string? sitemapXml)
        {
            try
            {
                var doc = await Xml.ParseFromFileAsync(sitemapXml);
                var result = doc.Descendants()
                    .Where(x => x.Name.LocalName == "loc")
                    .Select(e => e.Value)
                    .ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }

    public class TrainingExample
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("completion")]
        public string Completion { get; set; }
    }
}
