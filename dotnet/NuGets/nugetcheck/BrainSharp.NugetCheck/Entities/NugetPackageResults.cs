namespace BrainSharp.NugetCheck.Entities;

public class NugetPackageResults
{
    public string NugetPackageId { get; set; }
    public string? NugetPackageVersion { get; set; }
    public List<Warning> Warnings { get; set; }
}