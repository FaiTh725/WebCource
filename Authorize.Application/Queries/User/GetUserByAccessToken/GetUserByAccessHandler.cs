using Application.Shared.Exceptions;
using Authorize.Application.Contacts.User;
using Authorize.Domain.Repositories;
using MediatR;

namespace Authorize.Application.Queries.User.GetUserByAccessToken
{
    public class GetUserByAccessHandler :
        IRequestHandler<GetUserByAccessTokenQuery, UserResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserByAccessHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> Handle(GetUserByAccessTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository
                .GetRefreshTokenWithUser(request.AccessToken);
        
            if(refreshToken is null)
            {
                throw new NotFoundApiException("User doesnt exist");
            }

            return new UserResponse
            {
                Email = refreshToken.User.Email,
                RoleName = refreshToken.User.Role.RoleName
            };
        }
    }
}
