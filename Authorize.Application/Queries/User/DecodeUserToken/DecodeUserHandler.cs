using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using MediatR;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Queries.User.DecodeUserToken
{
    public class DecodeUserHandler : IRequestHandler<DecodeUserQuery, TokenResponse>
    {
        private readonly IJwtService<UserEntity, TokenResponse> jwtService;

        public DecodeUserHandler(
            IJwtService<UserEntity, TokenResponse> jwtService)
        {
            this.jwtService = jwtService;
        }

        public Task<TokenResponse> Handle(DecodeUserQuery request, CancellationToken cancellationToken)
        {
            var decodeToken = jwtService.DecodeToken(request.Token);

            if(decodeToken.IsFailure)
            {
                throw new InternalServerApiException("Error decode token");
            }

            return Task.FromResult(decodeToken.Value);
        }
    }
}
