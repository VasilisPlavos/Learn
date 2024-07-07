namespace BrainSharp.NugetCheck.Dtos;

public class DeprecationDto
{
    public string message { get; set; }
    public string[] reasons { get; set; }
}