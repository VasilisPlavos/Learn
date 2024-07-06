using System.Text.Json.Serialization;

namespace BrainSharp.NugetCheck.Dtos.ResponseDtos;

public class NugetVersionCatalogEntryResponseDto
{

    public class Rootobject
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
        public string[] type { get; set; }
        public string authors { get; set; }
        public string catalogcommitId { get; set; }
        public DateTime catalogcommitTimeStamp { get; set; }
        public string? copyright { get; set; }
        public DateTime created { get; set; }
        public DeprecationDto? deprecation { get; set; }
        public string description { get; set; }
        public string iconFile { get; set; }
        public bool isPrerelease { get; set; }
        public DateTime lastEdited { get; set; }
        public string licenseFile { get; set; }
        public string licenseUrl { get; set; }
        public bool listed { get; set; }
        public string packageHash { get; set; }
        public string packageHashAlgorithm { get; set; }
        public int packageSize { get; set; }
        public string projectUrl { get; set; }
        public DateTime published { get; set; }
        public string readmeFile { get; set; }
        public string releaseNotes { get; set; }
        public string repository { get; set; }
        public bool requireLicenseAcceptance { get; set; }
        public string title { get; set; }
        public string verbatimVersion { get; set; }
        public string version { get; set; }
        public Dependencygroup[] dependencyGroups { get; set; }
        public Packageentry[] packageEntries { get; set; }
        public string[] tags { get; set; }
        public VulnerabilityDto[] vulnerabilities { get; set; }
        public Context context { get; set; }
    }



    public class Context
    {
        public string vocab { get; set; }
        public string catalog { get; set; }
        public string xsd { get; set; }
        public Dependencies dependencies { get; set; }
        public Dependencygroups dependencyGroups { get; set; }
        public Packageentries packageEntries { get; set; }
        public Packagetypes packageTypes { get; set; }
        public Supportedframeworks supportedFrameworks { get; set; }
        public Tags tags { get; set; }
        public Vulnerabilities vulnerabilities { get; set; }
        public Published published { get; set; }
        public Created created { get; set; }
        public Lastedited lastEdited { get; set; }
        public CatalogCommittimestamp catalogcommitTimeStamp { get; set; }
        public Reasons reasons { get; set; }
    }

    public class Dependencies
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Dependencygroups
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Packageentries
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Packagetypes
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Supportedframeworks
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Tags
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Vulnerabilities
    {
        public string id { get; set; }
        public string container { get; set; }
    }

    public class Published
    {
        public string type { get; set; }
    }

    public class Created
    {
        public string type { get; set; }
    }

    public class Lastedited
    {
        public string type { get; set; }
    }

    public class CatalogCommittimestamp
    {
        public string type { get; set; }
    }

    public class Reasons
    {
        public string container { get; set; }
    }

    public class Dependencygroup
    {
        [JsonPropertyName("@id")]
        public string id { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }

        public Dependency[]? dependencies { get; set; }
        public string targetFramework { get; set; }
    }

    public class Dependency
    {
        public string type { get; set; }
        [JsonPropertyName("id")]
        public string PackageId { get; set; }
        public string range { get; set; }
    }

    public class Packageentry
    {
        public string id { get; set; }
        public string type { get; set; }
        public int compressedLength { get; set; }
        public string fullName { get; set; }
        public int length { get; set; }
        public string name { get; set; }
    }

}