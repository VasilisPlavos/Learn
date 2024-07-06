using BrainSharp.NugetCheck.Dtos.ResponseDtos;

namespace BrainSharp.NugetCheck.Dtos;

public class NugetPackageDto
{
    public string NugetPackageId { get; set; }
    public string Name { get; set; }
    public AzureSearchQueryResponseDto.Version[] Versions { get; set; }
}