using BrainSharp.NugetCheck.Dtos;

namespace BrainSharp.NugetCheck.Entities;

public class NugetPackage
{
    public DateTime DateScanned { get; set; }
    public string NugetPackageId { get; set; }
    public VersionDto[] Versions { get; set; }
}