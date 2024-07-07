# nugetcheck
 A quick way to scan Nuget packages for volnurabilities. View the source code on [GitHub](https://github.com/VasilisPlavos/Learn/tree/main/dotnet/NuGets/nugetcheck/).

There are 3 ways to use it:
1. `npx nugetcheck package SixLabors.ImageSharp --version 3.1.4` -> Checking package SixLabors.ImageSharp with version 3.1.4 and it's transitives
2. `npx nugetcheck test.csproj` -> Checking packages included in test.csproj file and it's transitives
3. `npx nugetcheck .` -> Finding all csproj in selected folder and subfolders. After checking packages included in each csproj file and it's transitives

Try these to see the differences:
- `npx nugetcheck package SixLabors.ImageSharp --version 3.1.4`
- `npx nugetcheck package SixLabors.ImageSharp --version 3.1.3`
- `npx nugetcheck package Microsoft.NET.Test.Sdk --version 17.3.2` -> if you want something taugh try this! But it can take more that 30 seconds the first time...