using Microsoft.Azure.Functions.Worker;
using System.Text.Json;

namespace HelloWorldCode.Helpers;

public class TokenHelper
{
    public static bool TryGetFromHeaders(FunctionContext context, out string? token)
    {
        token = null;

        // HTTP headers are in the binding context as a JSON object
        // The first checks ensure that we have the JSON string
        if (!context.BindingContext.BindingData.TryGetValue("Headers", out var headersObj)) return false;
        if (headersObj is not string headersStr) return false;

        // Deserialize headers from JSON
        var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(headersStr);
        var normalizedKeyHeaders = headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value);

        // No Authorization header present
        if (!normalizedKeyHeaders.TryGetValue("authorization", out var authHeaderValue)) return false;

        // Scheme is not Bearer
        if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) return false;

        token = authHeaderValue.Substring("Bearer ".Length).Trim();
        return true;
    }
}