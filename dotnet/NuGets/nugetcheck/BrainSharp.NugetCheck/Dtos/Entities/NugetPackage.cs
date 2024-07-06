using BrainSharp.NugetCheck.Dtos.ResponseDtos;

namespace BrainSharp.NugetCheck.Dtos.Entities;

public class NugetPackage
{
    public string NugetPackageId { get; set; }
    public string Name { get; set; }
    public AzureSearchQueryResponseDto.Version[] Versions { get; set; }
}