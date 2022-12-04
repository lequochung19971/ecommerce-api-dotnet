using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Ecommerce.Services;
public class AzureStorageAccountService : IAzureStorageAccountService
{
    private readonly string _azureConnectionString;
    private readonly BlobContainerClient _container;

    public AzureStorageAccountService(IConfiguration configuration)
    {
        _azureConnectionString = configuration.GetConnectionString("AzureConnectionString");
        _container = new BlobContainerClient(_azureConnectionString, "assets");
        var createResponse = _container.CreateIfNotExists();
        if(createResponse != null && createResponse.GetRawResponse().Status == 201)
                _container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
    }

    public async Task DeleteFileAsync(string blobName)
    {
        var blob = _container.GetBlobClient(blobName);
        await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
    }

    public async Task<BlobClient> UploadFileAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            var blob = _container.GetBlobClient($"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{file.FileName}");
            using (var fileStream = file.OpenReadStream())
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blob;
        }

        return null;
    }


}