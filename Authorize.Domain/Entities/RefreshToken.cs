using CSharpFunctionalExtensions;

namespace Authorize.Domain.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public long UserId { get; set; }
        public User User { get; init; }
    
        public DateTime ExpiresOn { get; set; }

        public RefreshToken(){}

        private RefreshToken(
            string token,
            User user,
            DateTime expiresOn)
        {
            Token = token;
            User = user;
            ExpiresOn = expiresOn;
        }

        private RefreshToken(
            string token,
            long userId,
            DateTime expiresOn)
        {
            Token = token;
            UserId = userId;
            ExpiresOn = expiresOn;
        }

        public static Result<RefreshToken> Initialize(
            string token,
            User user,
            DateTime expiresOn)
        {
            if(string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<RefreshToken>("Token is null");
            }

            if(user is null)
            {
                return Result.Failure<RefreshToken>("User is null");
            }

            if(expiresOn < DateTime.UtcNow)
            {
                return Result.Failure<RefreshToken>("ExpiresOn should be the future time");
            }

            return Result.Success(new RefreshToken(
                token, user, expiresOn));
        }

        public static Result<RefreshToken> Initialize(
            string token,
            long userId,
            DateTime expiresOn)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<RefreshToken>("Token is null");
            }

            if (expiresOn < DateTime.UtcNow)
            {
                return Result.Failure<RefreshToken>("ExpiresOn should be the future time");
            }

            return Result.Success(new RefreshToken(
                token, userId, expiresOn));
        }
    }
}
