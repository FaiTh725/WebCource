namespace Test.Application.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadBlob(string blobName, Stream stream, string contentType, CancellationToken cancellationToken = default);

        Task<string> GetBlobUrl(string path, CancellationToken cancellationToken = default);

        Task<List<string>> GetBlobFolder(string path, CancellationToken cancellationToken = default);

        Task DeleteBlob(string path, CancellationToken cancellationToken = default);

        Task DeleteBlobFolder(string folderPath, CancellationToken cancellationToken = default);
    }
}
