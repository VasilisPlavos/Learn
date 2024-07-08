using System.Net.Http.Json;
using System.Xml.Linq;
using BrainSharp.NugetCheck.Dtos;
using BrainSharp.NugetCheck.Dtos.ResponseDtos;
using BrainSharp.NugetCheck.Entities;
using BrainSharp.NugetCheck.Services;

namespace BrainSharp.NugetCheck;

public class NugetCheck
{
    private readonly List<PackageDto> _scannedPackages = [];
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
                BreadCrumb = $"{mainPackageName} {mainPackageVersion}",
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

                _scannedPackages.Add(new PackageDto
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
        var nugetPackage = await LocalStorageService.GetNugetPackageAsync(packageName);
        if (nugetPackage?.DateScanned > DateTime.UtcNow.AddDays(-1))
        {
            return nugetPackage;
        }

        var response = await _client.GetAsync($"https://azuresearch-usnc.nuget.org/query?q={packageName}");
        var responseDto = await response.Content.ReadFromJsonAsync<AzureSearchQueryResponseDto.Rootobject>();

        var unique = responseDto?.data.FirstOrDefault(x => x.PackageId.ToLower() == packageName.ToLower());
        if (unique == null) return null;

        nugetPackage = new NugetPackage
        {
            NugetPackageId = unique.PackageId,
            Versions = unique.versions,
            DateScanned = DateTime.UtcNow
        };

        await LocalStorageService.SaveNugetPackageAsync(nugetPackage);
        return nugetPackage;
    }

    public async Task<NugetPackageVersionInfo?> SearchPackageVersionInfoAsync(NugetPackage package, string packageVersion)
    {
        try
        {
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"Scanning {package.NugetPackageId} {packageVersion}");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        

        var nugetPackageVersionInfo = await LocalStorageService.GetNugetPackageVersionInfoAsync(package.NugetPackageId, packageVersion);
        if (nugetPackageVersionInfo?.DateScanned > DateTime.UtcNow.AddDays(-1))
        {
            return nugetPackageVersionInfo;
        }

        var versionInfoUrl = package.Versions.Where(x => x.version == packageVersion).Select(x => x.IndexUrl).FirstOrDefault();
        if (versionInfoUrl == null) return null; // this can mean that the package version does not exist or is not listed

        var versionIndexResponse = await _client.GetAsync(versionInfoUrl);
        var versionIndexResponseDto = await versionIndexResponse.Content.ReadFromJsonAsync<NugetVersionIndexResponseDto.Rootobject>();
        if (versionIndexResponseDto == null) return null;

        var versionCatalogEntryUrl = versionIndexResponseDto.catalogEntry;
        var versionCatalogEntryResponse = await _client.GetAsync(versionCatalogEntryUrl);
        var versionCatalogEntryResponseDto = await versionCatalogEntryResponse.Content.ReadFromJsonAsync<NugetVersionCatalogEntryResponseDto.Rootobject>();
        if (versionCatalogEntryResponseDto == null) return null;

        nugetPackageVersionInfo = new NugetPackageVersionInfo
        {
            NugetPackageId = package.NugetPackageId,
            CatalogEntry = versionIndexResponseDto.catalogEntry,
            DateScanned = DateTime.UtcNow,
            Deprecation = versionCatalogEntryResponseDto.deprecation,
            DependencyGroups = versionCatalogEntryResponseDto.dependencyGroups,
            Listed = versionIndexResponseDto.listed,
            IndexUrl = versionInfoUrl,
            Vulnerabilities = versionCatalogEntryResponseDto.vulnerabilities,
            Version = versionCatalogEntryResponseDto.version
        };

        await LocalStorageService.SaveNugetPackageVersionInfoAsync(nugetPackageVersionInfo);
        return nugetPackageVersionInfo;
    }

    private static List<PackageDto> GetProjectPackageList(string filePath) => GetProjectPackageList(XDocument.Load(filePath));
    private static List<PackageDto> GetProjectPackageList(XDocument csProjToXDocument)
    {
        var listOfPackages = new List<PackageDto>();
        var itemGroups = csProjToXDocument.Elements().ToList().Elements().ToList().Where(x => x.Name == "ItemGroup").ToList();
        foreach (var itemGroup in itemGroups)
        {
            foreach (var item in itemGroup.Elements().Where(x => x.Name == "PackageReference").ToList())
            {
                var inc = item.Attributes().ToList();
                var version = inc.Where(x => x.Name == "Version").Select(x => x.Value).FirstOrDefault();
                var packageName = inc.Where(x => x.Name == "Include").Select(x => x.Value).FirstOrDefault();
                listOfPackages.Add(new PackageDto
                {
                    Version = version!,
                    NugetPackageId = packageName!
                });
            }
        }

        return listOfPackages;
    }

    public async Task<ProjectResults> CheckPackageAndTransientsAsync(string projectFilePath)
    {
        var packageReferences = GetProjectPackageList(projectFilePath);

        var packageReferencesResults = new List<NugetPackageResults>();
        foreach (var package in packageReferences)
        {
            var packageResults = await CheckPackageAndTransientsAsync(package.NugetPackageId, package.Version);
            packageReferencesResults.Add(packageResults);
        }

        return new ProjectResults
        {
            ProjectFilePath = projectFilePath,
            PackageReferences = packageReferencesResults, 
            TotalWarnings = packageReferencesResults.Sum(package => package.Warnings.Count)
        };
    }
}