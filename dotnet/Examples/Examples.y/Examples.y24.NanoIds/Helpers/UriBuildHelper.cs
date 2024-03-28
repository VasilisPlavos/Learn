namespace Examples.y24.NanoIds.Helpers;

public static class UriBuildHelper
{
    public static UriBuilder Build(Uri baseUrl, Dictionary<string, string>? queryParameters, string? anchor)
    {
        var queryString = baseUrl.Query;
        if (queryParameters?.Count > 0)
        {
            foreach (var p in queryParameters)
            {
                queryString = $"{queryString}&{p.Key}={p.Value}";
            }
        }

        return new UriBuilder(baseUrl)
        {
            Query = queryString,
            Fragment = anchor
        };
    }
}