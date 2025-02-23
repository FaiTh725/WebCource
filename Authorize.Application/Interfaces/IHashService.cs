namespace Authorize.Application.Interfaces
{
    public interface IHashService
    {
        string GenerateHash(string inputString);

        bool VerifyHash(string inputString, string cache);
    }
}
