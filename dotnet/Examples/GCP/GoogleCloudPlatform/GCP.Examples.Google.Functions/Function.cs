using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GCP.Examples.Google.Functions;

// Define a class that implements the IHttpFunction interface
public class Function : IHttpFunction
{
    /// <summary>
    /// Logic for your function goes here.
    /// </summary>
    /// <param name="context">The HTTP context, containing the request and the response.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    // Try: http://localhost:8080/test?name=vasilis
    public async Task HandleAsync(HttpContext context)
    {
        var route = context.Request.Path.HasValue ? context.Request.Path.Value : "";
        context.Request.Query.TryGetValue("name", out var nameParam);
        await context.Response.WriteAsync($"Hello, {nameParam.ToString()}");
    }
}
