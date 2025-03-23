using Authorize.Domain.Entities;
using Authorize.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Authorize.Dal.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext context;

        public RefreshTokenRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            var refreshTokenEntity = await context.RefreshTokens
                .AddAsync(refreshToken);

            return refreshTokenEntity.Entity;
        }

        public async Task DeleteToken(long tokenId)
        {
            await context.RefreshTokens
                .Where(x => x.Id == tokenId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteToken(Expression<Func<RefreshToken, bool>> expression)
        {
            await context.RefreshTokens
                .Where(expression)
                .ExecuteDeleteAsync();
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<RefreshToken?> GetRefreshTokenByUserId(long userId)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<RefreshToken?> GetRefreshTokenWithUser(string token)
        {
            return await context.RefreshTokens
                .Include(x => x.User)
                    .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task UpdateRefreshToken(long tokenId, RefreshToken refreshTokenToUpdate)
        {
            await context.RefreshTokens
                .Where(x => x.Id == tokenId)
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Token, refreshTokenToUpdate.Token)
                .SetProperty(p => p.ExpiresOn, refreshTokenToUpdate.ExpiresOn));
        }
    }
}
