using System.Diagnostics;

// καθαρίζει τον φάκελο του build
var projectDir = "C:\\Users\\vplav\\Gits\\Learn\\dotnet\\NuGets\\nugetcheck\\BrainSharp.NugetCheck.Console";
var publishDir = $"{projectDir}\\bin\\Release\\net8.0\\publish";
if (Directory.Exists(publishDir)) Directory.Delete(publishDir, true);

// τρέχει το build
await ExecuteProcessAsync("dotnet", "build -c Release", projectDir);

// τρέχει το publish
await ExecuteProcessAsync("dotnet", "publish", projectDir);

// φτιάχνει την έκδοση
var npmFileName = "C:\\Program Files\\nodejs\\npm.cmd";
var npxSkeletonDir = "C:\\Users\\vplav\\Gits\\Learn\\dotnet\\NuGets\\nugetcheck\\npx\\npx-skeleton";
await ExecuteProcessAsync(npmFileName, "version patch", npxSkeletonDir);

// αντιγράφει τα αρχεία του skeleton εκεί που πρέπει
var npxBuildDir = "C:\\Users\\vplav\\Gits\\Learn\\dotnet\\NuGets\\nugetcheck\\npx\\nugetcheck\\build";
if (Directory.Exists(npxBuildDir)) Directory.Delete(npxBuildDir, true);
CopyDirectory(npxSkeletonDir, npxBuildDir);

// αντιγράφει τα αρχεία του build εκεί που πρέπει
var consoleAppDir = $"{npxBuildDir}\\consoleapp";
CopyDirectory(publishDir, consoleAppDir);

// κάνει το publish
await ExecuteProcessAsync(npmFileName, "publish", npxBuildDir);


async Task ExecuteProcessAsync(string fileName, string arguments, string workingDirectory)
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };

    process.Start();
    await process.WaitForExitAsync();
    Console.WriteLine(process.StandardOutput.ReadToEnd());
}

void CopyDirectory(string sourceDir, string destinationDir, bool recursive = true)
{
    var sourceDirInfo = new DirectoryInfo(sourceDir);
    if (!sourceDirInfo.Exists) throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");

    Directory.CreateDirectory(destinationDir);

    // Get the files in the source directory and copy to the destination directory
    foreach (FileInfo file in sourceDirInfo.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        file.CopyTo(targetFilePath);
    }

    // If recursive and copying subdirectories, recursively call this method on each subdirectory
    if (recursive)
    {
        DirectoryInfo[] subdirectories = sourceDirInfo.GetDirectories();
        foreach (DirectoryInfo subDir in subdirectories)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}



