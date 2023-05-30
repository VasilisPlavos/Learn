using System.Net;
using HelloWorldCode.Helpers.Attribute;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HelloWorldCode.Functions
{
    [Authorize(Scopes = new[] { "access_as_user" }, UserRoles = new[] { "user", "admin" })]
    public static class TestFunctions
    {
        //[Function("UsersAndAdmins")]
        public static HttpResponseData UsersAndAdmins
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, 
            FunctionContext executionContext
        )
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to UsersAndAdmins Functions!");
            return response;
        }

        //[Function("OnlyAdmins")]
        [Authorize(Scopes = new[] { "access_as_user" }, UserRoles = new[] { "admin" })]
        public static HttpResponseData OnlyAdmins
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext
        )
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to OnlyAdmins Functions!");
            return response;
        }
    }
}
