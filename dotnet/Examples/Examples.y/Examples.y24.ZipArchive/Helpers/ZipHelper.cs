using System.IO.Compression;

namespace Examples.y24.ZipArchive.Helpers
{
    internal static class ZipHelper
    {
        public static void ZipDirectoryOld(string directoryPath, string zipFilePath)
        {
            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory '{directoryPath}' does not exist.");
            }

            // Create the zip archive
            using var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
            // Recursively add files and directories to the archive
            ZipDirectoryInternal(zipArchive, directoryPath, "");
        }

        private static void ZipDirectoryInternal(System.IO.Compression.ZipArchive zipArchive, string directoryPath, string baseInArchive)
        {
            // Add files in the current directory
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                string entryName = Path.Combine(baseInArchive, Path.GetFileName(file));
                zipArchive.CreateEntryFromFile(file, entryName);
            }

            // Recursively add subdirectories
            foreach (string subdirectory in Directory.GetDirectories(directoryPath))
            {
                string subdirectoryPathInArchive = Path.Combine(baseInArchive, Path.GetFileName(subdirectory));
                ZipDirectoryInternal(zipArchive, subdirectory, subdirectoryPathInArchive);
            }
        }

        public static void ZipDirectoryNew(string sourceDirectoryName, string destinationArciveFileName, CompressionLevel compressionLevel, bool includeBaseDir)
        {
            // if zip exist?
            // if source dir not found?

            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArciveFileName, compressionLevel, includeBaseDir);
        }
    }
}
