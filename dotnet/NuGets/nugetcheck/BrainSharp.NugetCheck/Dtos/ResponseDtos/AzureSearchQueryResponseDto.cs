using System.Text.Json.Serialization;

namespace BrainSharp.NugetCheck.Dtos.ResponseDtos;

/// <summary>
/// The Response Dto from https://azuresearch-usnc.nuget.org/query?q=
/// </summary>
public class AzureSearchQueryResponseDto
{
    public class Rootobject
    {
        public Context context { get; set; }
        public int totalHits { get; set; }
        public Datum[] data { get; set; }
    }

    public class Context
    {
        public string vocab { get; set; }
        public string _base { get; set; }
    }

    public class Datum
    {
        /// <summary>
        /// This is the index url of the package (eg. https://api.nuget.org/v3/registration5-semver1/sixlabors.imagesharp/index.json)
        /// </summary>
        [JsonPropertyName("@id")]
        public string IndexUrl { get; set; }

        /// <summary>
        /// This is the package official id. (eg. SixLabors.ImageSharp)
        /// </summary>
        [JsonPropertyName("id")]
        public string PackageId { get; set; }
        public string type { get; set; }
        public string registration { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        public string? iconUrl { get; set; }
        public string licenseUrl { get; set; }
        public string projectUrl { get; set; }
        public string[] tags { get; set; }
        public string[] authors { get; set; }
        public string[] owners { get; set; }
        public long totalDownloads { get; set; }
        public bool verified { get; set; }
        public Packagetype[] packageTypes { get; set; }
        public Version[] versions { get; set; }
        public object[] vulnerabilities { get; set; }
        public Deprecation? deprecation { get; set; }
    }

    public class Deprecation
    {
        public string[] reasons { get; set; }
    }

    public class Packagetype
    {
        public string name { get; set; }
    }

    public class Version
    {
        public string version { get; set; }
        public int downloads { get; set; }

        /// <summary>
        /// This is the index url of the version (eg. https://api.nuget.org/v3/registration5-semver1/sixlabors.imagesharp/3.1.3.json)
        /// </summary>
        [JsonPropertyName("@id")]
        public string IndexUrl { get; set; }
    }
}