using System.Net.Http.Json;
using BrainSharp.NugetCheck.Dtos;
using BrainSharp.NugetCheck.Dtos.ResponseDtos;
using BrainSharp.NugetCheck.Entities;

namespace BrainSharp.NugetCheck;

public class NugetCheck
{
    private readonly List<ScannedPackageDto> _scannedPackages = [];
    private readonly HttpClient _client = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(1) // We are doing this because if DNS will change, out HttpClient will stop working
    });

    public async Task<NugetPackageResults> CheckPackageAndTransientsAsync(string mainPackageName, string mainPackageVersion)
    {
        var nugetPackageResults = new NugetPackageResults()
        {
            Warnings = []
        };

        var mainPackage = await SearchPackageAsync(mainPackageName);
        if (mainPackage == null)
        {
            nugetPackageResults.Warnings.Add(new Warning
            {
                BreadCrumb = $"{mainPackage} {mainPackageVersion}",
                Package = null,
                Message = "Package not found",
            });

            return nugetPackageResults;
        }

        nugetPackageResults.NugetPackageId = mainPackage.NugetPackageId;

        var mainPackageInfo = await SearchPackageVersionInfoAsync(mainPackage, mainPackageVersion);

        var mainBreadCrumb = $"{mainPackage.NugetPackageId} {mainPackageInfo?.Version}".Trim();
        nugetPackageResults.Warnings.AddRange(GetRootPackageWarnings(mainPackageInfo, mainBreadCrumb));
        nugetPackageResults.NugetPackageVersion = mainPackageInfo?.Version;

        if (mainPackageInfo?.DependencyGroups == null) return nugetPackageResults;

        var dependencyGroupsMessages = await GetDependencyGroupsMessagesAsync(mainPackageInfo.DependencyGroups, mainBreadCrumb);
        nugetPackageResults.Warnings.AddRange(dependencyGroupsMessages);
        return nugetPackageResults;
    }

    private async Task<List<Warning>> GetDependencyGroupsMessagesAsync(DependencyGroupDto[] dependencyGroups, string breadCrumb)
    {
        var warnings = new List<Warning>();
        foreach (var dependencyGroupDto in dependencyGroups)
        {
            if (dependencyGroupDto.dependencies == null) continue;

            foreach (var dependencyDto in dependencyGroupDto.dependencies)
            {
                var version = GetVersion(dependencyDto.range);
                var alreadyChecked = _scannedPackages.Any(x => x.NugetPackageId == dependencyDto.PackageId && x.Version == version);
                if (alreadyChecked) continue;

                _scannedPackages.Add(new ScannedPackageDto
                {
                    NugetPackageId = dependencyDto.PackageId,
                    Version = version
                });

                var packageToScan = await SearchPackageAsync(dependencyDto.PackageId);
                if (packageToScan == null)
                {
                    warnings.Add(new Warning
                    {
                        BreadCrumb = GetCurrentBreadCrumb(breadCrumb, dependencyDto.PackageId, version),
                        Package = null,
                        Message = "Package not found",
                    });
                    continue;
                }

                var packageInfoToScan = await SearchPackageVersionInfoAsync(packageToScan, version);
                warnings.AddRange(GetRootPackageWarnings(packageInfoToScan, GetCurrentBreadCrumb(breadCrumb, dependencyDto.PackageId, version)));

                if (packageInfoToScan?.DependencyGroups == null) continue;
                var packageInfoToScanWarnings = await GetDependencyGroupsMessagesAsync(packageInfoToScan.DependencyGroups, GetCurrentBreadCrumb(breadCrumb, dependencyDto.PackageId, version));
                warnings.AddRange(packageInfoToScanWarnings);
            }
        }


        return warnings;
    }

    private static string GetCurrentBreadCrumb(string breadCrumb, string packageId, string packageVersion)
    {
        return $"{breadCrumb} > {packageId} {packageVersion}";
    }

    private string GetVersion(string dependencyDtoRangeValue)
    {
        var value = dependencyDtoRangeValue.Split(",")[0];
        value = value.Replace(">", "").Replace("[", "").Replace(")", "").Replace(",", "").Trim();
        return value;
    }

    private bool IsDeprecated(NugetPackageVersionInfo packageInfo)
    {
        return packageInfo.Deprecation != null;
    }

    public async Task<bool?> IsDeprecatedAsync(string packageName, string packageVersion)
    {
        var package = await SearchPackageAsync(packageName);
        if (package == null) return null;

        var packageInfo = await SearchPackageVersionInfoAsync(package, packageVersion);
        if (packageInfo == null) return null;
        return IsDeprecated(packageInfo);
    }

    private bool IsListed(NugetPackageVersionInfo? packageInfo)
    {
        if (packageInfo == null) return false;
        return packageInfo.Listed;
    }

    public async Task<bool?> IsListedAsync(string packageName, string packageVersion)
    {
        var package = await SearchPackageAsync(packageName);
        if (package == null) return null;

        var packageInfo = await SearchPackageVersionInfoAsync(package, packageVersion);
        return IsListed(packageInfo);
    }

    private bool IsVulnerable(NugetPackageVersionInfo packageInfo)
    {
        return packageInfo.Vulnerabilities != null;
    }

    public async Task<bool?> IsVulnerableAsync(string packageName, string packageVersion)
    {
        var package = await SearchPackageAsync(packageName);
        if (package == null) return null;

        var packageInfo = await SearchPackageVersionInfoAsync(package, packageVersion);
        if (packageInfo == null) return null;
        return IsVulnerable(packageInfo);
    }

    private List<Warning> GetRootPackageWarnings(NugetPackageVersionInfo? packageInfo, string currentBreadCrumb)
    {
        var warnings = new List<Warning>();
        if (!IsListed(packageInfo))
        {
            warnings.Add(new Warning
            {
                Message = "Package is not listed",
                BreadCrumb = currentBreadCrumb,
                Package = packageInfo
            });
        }

        // if package info not found we cannot check for vulnerabilities or deprecation
        if (packageInfo == null) return warnings;

        if (IsVulnerable(packageInfo))
        {
            warnings.Add(new Warning
            {
                Message = "Package is vulnerable",
                BreadCrumb = currentBreadCrumb,
                Package = packageInfo
            });
        }

        if (IsDeprecated(packageInfo))
        {
            warnings.Add(new Warning
            {
                Message = "Package is deprecated",
                BreadCrumb = currentBreadCrumb,
                Package = packageInfo
            });
        }

        return warnings;
    }

    public async Task<NugetPackage?> SearchPackageAsync(string packageName)
    {
        var response = await _client.GetAsync($"https://azuresearch-usnc.nuget.org/query?q={packageName}");
        var responseDto = await response.Content.ReadFromJsonAsync<AzureSearchQueryResponseDto.Rootobject>();

        var unique = responseDto?.data.FirstOrDefault(x => x.PackageId.ToLower() == packageName.ToLower());
        if (unique == null) return null;

        var nugetPackage = new NugetPackage
        {
            NugetPackageId = unique.PackageId,
            Versions = unique.versions
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
            Deprecation = versionCatalogEntryResponseDto.deprecation,
            DependencyGroups = versionCatalogEntryResponseDto.dependencyGroups,
            Listed = versionIndexResponseDto.listed,
            IndexUrl = versionInfoUrl,
            Vulnerabilities = versionCatalogEntryResponseDto.vulnerabilities,
            Version = versionCatalogEntryResponseDto.version
        };

        return nugetPackageVersionInfo;
    }
}