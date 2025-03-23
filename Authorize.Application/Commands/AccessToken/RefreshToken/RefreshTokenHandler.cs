using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MediatR;
using RefreshTokenEntity = Authorize.Domain.Entities.RefreshToken;

namespace Authorize.Application.Commands.AccessToken.RefreshToken
{
    public class RefreshTokenHandler :
        IRequestHandler<RefreshTokenCommand, string>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService<GenerateUserToken, TokenResponse> jwtService;

        public RefreshTokenHandler(
            IUnitOfWork unitOfWork,
            IJwtService<GenerateUserToken, TokenResponse> jwtService)
        {
            this.unitOfWork = unitOfWork;   
            this.jwtService = jwtService;
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await unitOfWork.RefreshTokenRepository
                .GetRefreshToken(request.RefreshToken);

            if(token is null ||
                token.ExpiresOn < DateTime.UtcNow)
            {
                throw new ForbiddenAccessApiException("RefreshToken doesnt exist or expired");
            }

            var refreshedTokenEntity = RefreshTokenEntity.Initialize(
                jwtService.GenerateRefreshToken(),
                token.UserId, 
                DateTime.UtcNow.AddDays(7));

            if(refreshedTokenEntity.IsFailure)
            {
                throw new InternalServerApiException("Error create new refresh token");
            }

            await unitOfWork.BeginTransactionAsync();

            await unitOfWork.RefreshTokenRepository
                .DeleteToken(token.Id);

            var dbRefreshToken = await unitOfWork.RefreshTokenRepository
                .AddRefreshToken(refreshedTokenEntity.Value);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            return dbRefreshToken.Token;
        }
    }
}
