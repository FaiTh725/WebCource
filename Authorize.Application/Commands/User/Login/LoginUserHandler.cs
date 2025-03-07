using Application.Shared.Exceptions;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MediatR;

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

            if(user is null)
            {
                throw new NotFoundApiException("Email is not exist");
            }

            if (!hashService.VerifyHash(
                request.Password,
                user.PasswordHash))
            {
                throw new BadRequestApiException("Invaliad Email Or Password");
            }

            return user.Id;
        }
    }
}
