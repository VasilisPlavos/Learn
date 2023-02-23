using System.Text.Json;
var credentials = LoadCredentials();
Console.WriteLine($"AWS key is {credentials?.AwsKey} and GCP Maps Key is {credentials?.GcpMapsKey}");
credentials = await LoadCredentialsAsync();
Console.WriteLine($"AWS key is {credentials?.AwsKey} and GCP Maps Key is {credentials?.GcpMapsKey}");

CredentialsDto? LoadCredentials()
{
    using (StreamReader r = new StreamReader(".\\credentials.json"))
    {
        string json = r.ReadToEnd();
        return JsonSerializer.Deserialize<CredentialsDto>(json);
    }
}

async Task<CredentialsDto?> LoadCredentialsAsync()
{
    using var r = new StreamReader(".\\credentials.json");
    string json = await r.ReadToEndAsync();
    return JsonSerializer.Deserialize<CredentialsDto>(json);
}