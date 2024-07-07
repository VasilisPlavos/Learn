using BrainSharp.NugetCheck.Entities;
using Newtonsoft.Json;

namespace BrainSharp.NugetCheck.Services;

public class LocalStorageService
{
    private static readonly string StoragePath = Path.Combine(AppContext.BaseDirectory, "storage");
    
    public static async Task<NugetPackage?> GetNugetPackageAsync(string packageName)
    {
        var filePath = Path.Combine(StoragePath, packageName, "index.json");
        try
        {
            using var sr = new StreamReader(filePath);
            var json = await sr.ReadToEndAsync();
            var nugetPackage = JsonConvert.DeserializeObject<NugetPackage>(json);
            return nugetPackage;
        }
        catch
        {
            // ignored
        }

        return null;
    }

    public static async Task<NugetPackageVersionInfo?> GetNugetPackageVersionInfoAsync(string packageName, string packageVersion)
    {
        var filePath = Path.Combine(StoragePath, packageName, $"{packageVersion}.json");
        try
        {
            using var sr = new StreamReader(filePath);
            var json = await sr.ReadToEndAsync();
            var nugetPackageVersionInfo = JsonConvert.DeserializeObject<NugetPackageVersionInfo>(json);
            return nugetPackageVersionInfo;
        }
        catch
        {
            // ignored
        }

        return null;
    }

    public static async Task SaveNugetPackageAsync(NugetPackage nugetPackage)
    {
        try
        {
            var directory = Path.Combine(StoragePath, nugetPackage.NugetPackageId);
            Directory.CreateDirectory(directory);

            var filePath = Path.Combine(directory, "index.json");
            var json = JsonConvert.SerializeObject(nugetPackage);
            await using var sw = new StreamWriter(filePath);
            await sw.WriteAsync(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static async Task SaveNugetPackageVersionInfoAsync(NugetPackageVersionInfo nugetPackageVersionInfo)
    {
        try
        {
            var directory = Path.Combine(StoragePath, nugetPackageVersionInfo.NugetPackageId);
            Directory.CreateDirectory(directory);

            var filePath = Path.Combine(directory, $"{nugetPackageVersionInfo.Version}.json");
            var json = JsonConvert.SerializeObject(nugetPackageVersionInfo);
            await using var sw = new StreamWriter(filePath);
            await sw.WriteAsync(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}