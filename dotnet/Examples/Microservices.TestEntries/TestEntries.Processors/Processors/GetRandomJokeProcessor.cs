using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestEntries.Common.Contracts;
using TestEntries.Common.Services;
using TestEntries.Contracts.Contracts.Joke;

namespace TestEntries.Processors.Processors
{
    public class GetRandomJokeProcessor
    {
        static readonly ContractHttpClient Client = new();

        public static async Task<IActionResult> GetResponseAsync(JokeRequestPackage request)
        {
            var endpointUrl = "https://api.chucknorris.io/jokes/random";
            
            var payload = JObject.FromObject(request.RequestData);
            var response = await Client.SendRequestAsync(HttpMethod.Get, endpointUrl, payload);
            return await DecerializeResponseAsync(endpointUrl, response);
        }

        private static async Task<IActionResult> DecerializeResponseAsync(string endpointUrl, HttpResponseMessage response)
        {
            var responsePackage = new JokeResponsePackage
            {
                ApprovalHeader = "apphead",
                EnvHeader = "envhead",
                LogHeader = "loghead",
                ResponseHeader = new ResponseHeader
                {
                    ResultStatus = response.IsSuccessStatusCode
                }
            };

            if (!response.IsSuccessStatusCode)
            {
                responsePackage.ResponseHeader.Errors = await PostErrorAsync(endpointUrl, response);
                return new JsonResult(responsePackage);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<JokeResponseContract>(responseString);
            responsePackage.ResponseData = responseData;

            return new JsonResult(responsePackage); ;
        }
        
        private static async Task<List<Error>> PostErrorAsync(string endpointUrl, HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var error = $"Client.SendRequestAsync: {response.RequestMessage?.Method.Method} {response.RequestMessage?.RequestUri?.AbsoluteUri} return errors. Response:{responseString}";
            var errors = new List<Error>()
            {
                new()
                {
                    Component = "this.GetType().Name",
                    Description = error,
                    ErrorCode = (int)response.StatusCode,
                    ErrorType = "enum ErrorTypes.TechnicalError",
                    SeverityLevel = "enum ErrorSeverity.Error",
                    SourceSystem = "enum Systems.Channel",
                    Suggestion = "enum Suggestions.Invalid",
                    TechnicalDescription = response.ReasonPhrase
                }
            };

            return errors;
        }
    }
}
