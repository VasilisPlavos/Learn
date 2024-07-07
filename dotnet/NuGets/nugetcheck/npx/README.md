# nugetcheck
 A quick way to scan Nuget packages for volnurabilities

There are 3 ways to use it:
1. `npx nugetcheck package SixLabors.ImageSharp --version 3.1.4` -> Checking package SixLabors.ImageSharp with version 3.1.4 and it's transitives
2. `npx nugetcheck test.csproj` -> Checking packages included in test.csproj file and it's transitives
3. `npx nugetcheck .` -> Finding all csproj in selected folder and subfolders. After checking packages included in each csproj file and it's transitives

Try these to see the differences:
- `npx nugetcheck package SixLabors.ImageSharp --version 3.1.4`
- `npx nugetcheck package SixLabors.ImageSharp --version 3.1.3`