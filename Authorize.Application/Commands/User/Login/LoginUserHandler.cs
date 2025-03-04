using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MediatR;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Commands.User.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, long>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;

        public LoginUserHandler(
            IUnitOfWork unitOfWork,
            IHashService hashService)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
        }

        public async Task<long> Handle(LoginUserRequest request, CancellationToken cancellationToken)
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

            return user.Value.Id;
        }
    }
}
