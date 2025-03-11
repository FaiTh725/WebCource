namespace Test.Application.Contracts.File
{
    public class FileEntity
    {
        public required Stream Stream { get; set; }

        public string ContentType { get; set; } = string.Empty;
    }
}
