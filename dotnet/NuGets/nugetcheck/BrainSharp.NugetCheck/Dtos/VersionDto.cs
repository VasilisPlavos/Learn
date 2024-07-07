using System.Text.Json.Serialization;

namespace BrainSharp.NugetCheck.Dtos;

public class VersionDto
{
    public string version { get; set; }
    public int downloads { get; set; }

    /// <summary>
    /// This is the index url of the version (eg. https://api.nuget.org/v3/registration5-semver1/sixlabors.imagesharp/3.1.3.json)
    /// </summary>
    [JsonPropertyName("@id")]
    public string IndexUrl { get; set; }
}