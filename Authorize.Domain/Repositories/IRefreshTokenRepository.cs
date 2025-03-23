using Authorize.Domain.Entities;
using System.Linq.Expressions;

namespace Authorize.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);

        Task<RefreshToken?> GetRefreshToken(string token);

        Task<RefreshToken?> GetRefreshTokenWithUser(string token);

        Task<RefreshToken?> GetRefreshTokenByUserId(long userId);

        Task UpdateRefreshToken(long tokenId, RefreshToken refreshTokenToUpdate);

        Task DeleteToken(long tokenId);

        Task DeleteToken(Expression<Func<RefreshToken, bool>> expression);

    }
}
