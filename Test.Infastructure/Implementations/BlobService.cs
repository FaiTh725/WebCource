using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Test.Application.Interfaces;

namespace Test.Infastructure.Implementations
{
    public class BlobService : IBlobService
    {
        private const string IMAGE_CONTAINER = "images";
        private readonly BlobContainerClient blobContainerClient;

        public BlobService(
            BlobServiceClient blobServiceClient)
        {
            blobContainerClient = blobServiceClient.GetBlobContainerClient(IMAGE_CONTAINER);
            blobContainerClient.CreateIfNotExists();
            blobContainerClient.SetAccessPolicy(PublicAccessType.Blob);

        }

        public async Task DeleteBlob(string path, CancellationToken cancellationToken = default)
        {
            BlobClient blobClient = blobContainerClient
                .GetBlobClient(path);

            await blobClient.DeleteIfExistsAsync(cancellationToken:cancellationToken);
        }

        public async Task DeleteBlobFolder(string folderPath, CancellationToken cancellationToken = default)
        {
            await foreach(var blobItem in 
                blobContainerClient.GetBlobsAsync(prefix: folderPath))
            {
                var blobClient = blobContainerClient
                    .GetBlobClient(blobItem.Name);

                await blobClient.DeleteIfExistsAsync();
            }
        }

        public async Task<string> GetBlobUrl(string path, CancellationToken cancellationToken = default)
        {
            BlobClient blobClient = blobContainerClient
                .GetBlobClient(path);
            
            if (await blobClient.ExistsAsync(cancellationToken))
            {
                return blobClient.Uri.AbsoluteUri;
            }

            return string.Empty;
        }

        public async Task<string> UploadBlob(string blobName, Stream stream, string contentType, CancellationToken cancellationToken = default)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(
                stream,
                new BlobHttpHeaders { ContentType = contentType},
                cancellationToken: cancellationToken);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
