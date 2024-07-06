using BrainSharp.NugetCheck.Dtos.ResponseDtos;

namespace BrainSharp.NugetCheck.Entities;

public class NugetPackage
{
    public string NugetPackageId { get; set; }
    public AzureSearchQueryResponseDto.Version[] Versions { get; set; }
}