using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace HelloWorldCode.Functions
{
    public class HelloFunction
    {
        private readonly ILogger _logger;

        public HelloFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HelloFunction>();
        }

        [Function("HelloFunction")]
        public HttpResponseData Run
        (
            [HttpTrigger(AuthorizationLevel.Admin, "get")] HttpRequestData req,
            FunctionContext executionContext
        )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to rAzure Functions!");

            return response;
        }
    }
}
