using Authorize.Domain.Repositories;

namespace Authorize.Application.Common.Hangfire
{
    public class HangFireWrapper
    {
        private readonly IUnitOfWork unitOfWork;

        public HangFireWrapper(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task DeleteExpiredTokenWrapper()
        {
            await unitOfWork.RefreshTokenRepository
                .DeleteToken(x => x.ExpiresOn < DateTime.UtcNow);
        }
    }
}
