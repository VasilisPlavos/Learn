using Newtonsoft.Json;
using System.Text;

namespace TestEntries.Common.Services
{
    public class ContractHttpClient : HttpClient
    {
        public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string endpointUrl, object payload)
        {
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(method, endpointUrl);
            requestMessage.Content = httpContent;
            var client = new HttpClient();
            var response = await client.SendAsync(requestMessage);
            return response;
        }
    }
}
