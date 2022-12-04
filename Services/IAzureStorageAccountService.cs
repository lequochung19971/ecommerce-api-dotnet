using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace Ecommerce.Services;
public interface IAzureStorageAccountService
{
    Task<BlobClient> UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string blogName);
}