using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MediatR;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Commands.User.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;
        private readonly IJwtService<UserEntity, TokenResponse> jwtService;

        public LoginUserHandler(
            IUnitOfWork unitOfWork,
            IHashService hashService,
            IJwtService<UserEntity, TokenResponse> jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.jwtService = jwtService;
        }

        public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository
                .GetUser(request.Email);

            if(user.IsFailure)
            {
                throw new NotFoundApiException("Email is not exist");
            }

            if (!hashService.VerifyHash(
                request.Password,
                user.Value.PasswordHash))
            {
                throw new BadRequestApiException("Invaliad Email Or Password");
            }

            var token = jwtService.GenerateToken(user.Value);

            return token;
        }
    }
}
