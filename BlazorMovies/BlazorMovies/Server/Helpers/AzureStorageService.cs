using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace BlazorMovies.Server.Helpers
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;

        public AzureStorageService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }

        public async Task<string> SaveFile(byte[] content, string extention, string containerName)
        {
            // Get a reference to the container
            var container = new BlobContainerClient(this.connectionString, containerName);
            // Create the container if it does not already exist
            await container.CreateIfNotExistsAsync();
            // Set the container's public access level    
            await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            // Give unique name of the file
            var filename = $"{Guid.NewGuid()}.{extention}";
            // Get a reference to a blob
            var blob = container.GetBlobClient(filename);
            // Upload local file
            await blob.UploadAsync(BinaryData.FromBytes(content));
            // Return URI of the stored image
            return blob.Uri.ToString();
        }        

        public async Task<string> EditFile(byte[] content, string extention, string containerName, string fileRoute)
        {
            if(!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }

            return await SaveFile(content, extention, containerName);
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            var container = new BlobContainerClient(this.connectionString, containerName);
            var blobName = Path.GetFileName(fileRoute);
            var blob = container.GetBlobClient(blobName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
