using System.Net.Http.Json;
using BrainSharp.NugetCheck.Dtos.ResponseDtos;
using BrainSharp.NugetCheck.Entities;

namespace BrainSharp.NugetCheck;

public class NugetCheck
{
    private readonly HttpClient _client = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(1) // We are doing this because if DNS will change, out HttpClient will stop working
    });

    public async Task<NugetPackage?> SearchPackageAsync(string packageName)
    {
        var response = await _client.GetAsync($"https://azuresearch-usnc.nuget.org/query?q={packageName}");
        var responseDto = await response.Content.ReadFromJsonAsync<AzureSearchQueryResponseDto.Rootobject>();

        var unique = responseDto?.data.FirstOrDefault(x => x.PackageId.ToLower() == packageName.ToLower());
        if (unique == null) return null;

        var nugetPackage = new NugetPackage
        {
            NugetPackageId = unique.PackageId,
            Versions =  unique.versions
        };
        
        return nugetPackage;
    }

    public async Task<NugetPackageVersionInfo?> SearchPackageVersionInfoAsync(NugetPackage package, string packageVersion)
    {
        var versionInfoUrl = package.Versions.Where(x => x.version == packageVersion).Select(x => x.IndexUrl).FirstOrDefault();
        if (versionInfoUrl == null) return null; // this can mean that the package version does not exist or is not listed

        var versionIndexResponse = await _client.GetAsync(versionInfoUrl);
        var versionIndexResponseDto = await versionIndexResponse.Content.ReadFromJsonAsync<NugetVersionIndexResponseDto.Rootobject>();
        if (versionIndexResponseDto == null) return null;

        var versionCatalogEntryUrl = versionIndexResponseDto.catalogEntry;
        var versionCatalogEntryResponse = await _client.GetAsync(versionCatalogEntryUrl);
        var versionCatalogEntryResponseDto = await versionCatalogEntryResponse.Content.ReadFromJsonAsync<NugetVersionCatalogEntryResponseDto.Rootobject>();
        if (versionCatalogEntryResponseDto == null) return null;

        var nugetPackageVersionInfo = new NugetPackageVersionInfo
        {
            NugetPackageId = package.NugetPackageId,
            CatalogEntry = versionIndexResponseDto.catalogEntry,
            Listed = versionIndexResponseDto.listed,
            IndexUrl = versionInfoUrl,
            Vulnerabilities =  versionCatalogEntryResponseDto.vulnerabilities,
            Version =  versionCatalogEntryResponseDto.version
        };

        return nugetPackageVersionInfo;
    }

    public async Task<bool?> IsVulnerable(string packageName, string packageVersion)
    {
        var package = await SearchPackageAsync(packageName);
        if (package == null) return null;

        var packageInfo = await SearchPackageVersionInfoAsync(package, packageVersion);
        if (packageInfo == null) return null;

        return packageInfo.Vulnerabilities != null;
    }

    public async Task<bool?> IsListed(string packageName, string packageVersion)
    {
        var package = await SearchPackageAsync(packageName);
        if (package == null) return null;

        var packageInfo = await SearchPackageVersionInfoAsync(package, packageVersion);
        if (packageInfo == null) return false;

        return packageInfo.Listed;
    }
}