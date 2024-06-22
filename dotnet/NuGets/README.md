# Nuget Notes

### My packages
https://www.nuget.org/profiles/vasilisplavos

### Quickstart: Create and publish a package with the dotnet CLI
https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli


### Cheatsheet

## 1. Create a class library project
1. Create a folder (ex. NewLibrary)
2. Open CMD in this new folder
3. Enter `dotnet new classlib`, which creates a project with the current folder name.

## 2. Add package metadata to the project file

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <!-- This is the name of the NuGet package in the Nuget.org Gallery -->
    <PackageId>BrainSharp.XmlDiff</PackageId>
    <Version>1.0.2</Version>
    <Authors>Vasilis Plavos</Authors>
    <Company>BrainSharp</Company>
    <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  </PropertyGroup>

</Project>
```

### 3. The commands you always need
```dotnetcli
dotnet build

dotnet publish

dotnet pack
```


## 4. Publish the package
```
dotnet nuget push .\bin\Release\BrainSharp.XmlDiff.1.0.2.nupkg --api-key qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 --source https://api.nuget.org/v3/index.json
```

