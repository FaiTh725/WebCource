using Authorize.Application.Interfaces;

namespace Authorize.Application.Implementations
{
    public class HashService : IHashService
    {
        public string GenerateHash(string inputString)
        {
            return BCrypt.Net.BCrypt.HashPassword(inputString);
        }

        public bool VerifyHash(string inputString, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(inputString, hash);
        }
    }
}
