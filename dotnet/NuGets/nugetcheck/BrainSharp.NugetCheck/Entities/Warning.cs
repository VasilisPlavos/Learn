namespace BrainSharp.NugetCheck.Entities;

public class Warning
{
    public string Message { get; set; }
    public string BreadCrumb { get; set; }
    public NugetPackageVersionInfo? Package { get; set; }
}