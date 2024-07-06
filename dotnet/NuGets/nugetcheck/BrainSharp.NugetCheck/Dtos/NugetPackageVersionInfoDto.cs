using BrainSharp.NugetCheck.Dtos.ResponseDtos;

namespace BrainSharp.NugetCheck.Dtos;

public class NugetPackageVersionInfoDto
{
    public string PackageName { get; set; }
    public string CatalogEntry { get; set; }
    public bool Listed { get; set; }
    public NugetVersionIndexResponseDto.Rootobject DatabaseVersionIndexResponse { get; set; }
    public NugetVersionCatalogEntryResponseDto.Vulnerability[]? Vulnerabilities { get; set; }
    public string Version { get; set; }
}