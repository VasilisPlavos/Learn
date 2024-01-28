using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Examples.y24.Azure.StorageAccount.Dtos;

namespace Examples.y24.Azure.StorageAccount.Services;

public interface IStorageAccountService
{
    Task RunAsync();
}

public class StorageAccountService : IStorageAccountService
{
    private readonly BlobServiceClient _blobServiceClient;

    public StorageAccountService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task RunAsync()
    {
        var blobs = FindBlobsByTags();
    }

    private List<TaggedBlobItem> FindBlobsByTags()
    {
        var query = $@"""{ImageTag.Status}""='{ImageStatus.Deleted}'";
        var blobs = _blobServiceClient.FindBlobsByTags(query).ToList();
        return blobs;
    }
}