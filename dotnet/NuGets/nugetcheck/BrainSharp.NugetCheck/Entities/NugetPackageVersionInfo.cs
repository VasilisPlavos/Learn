using BrainSharp.NugetCheck.Dtos;

namespace BrainSharp.NugetCheck.Entities;

public class NugetPackageVersionInfo
{
    public string NugetPackageId { get; set; }
    public string CatalogEntry { get; set; }
    public bool Listed { get; set; }
    public string Version { get; set; }
    public string IndexUrl { get; set; }
    public VulnerabilityDto[]? Vulnerabilities { get; set; }
    public DeprecationDto? Deprecation { get; set; }
    public DependencyGroupDto[] DependencyGroups { get; set; }
    public DateTime DateScanned { get; set; }
}