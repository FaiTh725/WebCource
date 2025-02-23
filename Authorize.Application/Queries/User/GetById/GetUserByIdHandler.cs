using Application.Shared.Exceptions;
using Authorize.Application.Queries.User.Responses;
using Authorize.Domain.Repositories;
using MediatR;

namespace Authorize.Application.Queries.User.GetById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdHandler(
            IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUser(request.Id);

            if (user.IsFailure)
            {
                throw new NotFoundApiException("User with this id doesnt exist");
            }

            return new UserResponse 
            { 
                Email = user.Value.Email,
                RoleName = user.Value.Role.RoleName
            };
        }
    }
}
