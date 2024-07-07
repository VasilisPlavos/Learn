using System.Text.Json.Serialization;

namespace BrainSharp.NugetCheck.Dtos;

public class DependencyGroupDto
{
    [JsonPropertyName("@id")]
    public string id { get; set; }

    [JsonPropertyName("@type")]
    public string type { get; set; }

    public DependencyDto[]? dependencies { get; set; }
    public string targetFramework { get; set; }
}