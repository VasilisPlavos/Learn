// If an Authorize attribute is placed at class-level,
// requests to any function within the class
// must pass the authorization checks
using HelloWorldCode.Helpers.Attribute;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Net;

[Authorize
(
    Scopes = new[] { "access_as_user" }, 
    UserRoles = new[] { "user", "admin" },
    AppRoles = new[] { "access_all_functions" }
 )]
public static class TestFunctions
{
    // This function can be called with both scopes and app roles
    // We don't need another Authorize attribute since it would just
    // contain the same values.
    [Function("ScopesAndAppRoles")]
    public static HttpResponseData ScopesAndAppRoles(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return CreateOkTextResponse(req, "Can be called with scopes or app roles");
    }

    // This function can only be called with scopes
    [Authorize(Scopes = new[] { "access_as_user" }, UserRoles = new[] { "user", "admin" })]
    [Function("OnlyScopes")]
    public static HttpResponseData OnlyScopes(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return CreateOkTextResponse(req, "Can be called with scopes only");
    }

    // This function can only be called with app roles
    [Authorize(AppRoles = new[] { "access_all_functions" })]
    [Function("OnlyAppRoles")]
    public static HttpResponseData OnlyAppRoles(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return CreateOkTextResponse(req, "Can be called with app roles only");
    }

    // This function can only be called with scopes + admin role
    [Authorize(Scopes = new[] { "access_as_user" }, UserRoles = new[] { "admin" })]
    [Function("OnlyAdmin")]
    public static HttpResponseData OnlyAdmin(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return CreateOkTextResponse(req, "Can be called with scopes and admin user only");
    }

    private static HttpResponseData CreateOkTextResponse(
        HttpRequestData request,
        string text)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(text);
        return response;
    }
}