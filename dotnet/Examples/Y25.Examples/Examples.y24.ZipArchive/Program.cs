// See https://aka.ms/new-console-template for more information

using Examples.y24.ZipArchive.Helpers;
using System.IO.Compression;

Console.WriteLine("Hello, World!");

var directoryPathToZip = Path.Combine(AppContext.BaseDirectory, "DirectoryToZip");

var zipFilePath = Path.Combine(AppContext.BaseDirectory, $"zip-{DateTime.Now.Ticks}.zip");
ZipHelper.ZipDirectoryNew(directoryPathToZip, zipFilePath, CompressionLevel.SmallestSize, false);

zipFilePath = Path.Combine(AppContext.BaseDirectory, $"zip-{DateTime.Now.Ticks}.zip");
ZipHelper.ZipDirectoryOld(directoryPathToZip, zipFilePath);