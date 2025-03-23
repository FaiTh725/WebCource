using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using RefreshTokenEntity = Authorize.Domain.Entities.RefreshToken;
using Authorize.Domain.Repositories;
using MediatR;

namespace Authorize.Application.Commands.AccessToken.CreateRefreshToken
{
    public class CreateRefreshTokenHandler :
        IRequestHandler<CreateRefreshTokenCommand, string>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService<GenerateUserToken, TokenResponse> jwtService;

        public CreateRefreshTokenHandler(
            IUnitOfWork unitOfWork,
            IJwtService<GenerateUserToken, TokenResponse> jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
        }

        public async Task<string> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetUser(request.UserId);

            if (user is null)
            {
                throw new BadRequestApiException("User doesnt exist");
            }

            var refreshToken = jwtService.GenerateRefreshToken();

            var refreshTokenEntity = RefreshTokenEntity.Initialize(
                refreshToken, user, DateTime.UtcNow.AddDays(7));

            if(refreshTokenEntity.IsFailure)
            {
                throw new BadRequestApiException(refreshTokenEntity.Error);
            }

            var dbRefreshToken = await unitOfWork
                .RefreshTokenRepository
                .AddRefreshToken(refreshTokenEntity.Value);

            await unitOfWork.SaveChangesAsync();    

            return dbRefreshToken.Token;
        }
    }
}
