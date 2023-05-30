using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;

namespace HelloWorldCode.Functions
{
    public class TestFunctions
    {
        //[Authorize(
        //    Scopes = new[] { "access_as_user" },
        //    UserRoles = new[] { "admin" })]
        public static HttpResponseData OnlyAdmins(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to test Functions!");

            return response;
        }
    }
}
