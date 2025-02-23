using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MediatR;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Commands.User.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, string>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;
        private readonly IJwtService<UserEntity, TokenResponse> jwtService;

        public RegisterUserHandler(
            IUnitOfWork unitOfWork,
            IHashService hashService,
            IJwtService<UserEntity, TokenResponse> jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.jwtService = jwtService;
        }

        public async Task<string> Handle(
            RegisterUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetUser(request.Email);

            if(user.IsSuccess)
            {
                throw new ConflictApiException("Email already registered");
            }

            if(!UserEntity.IsValidPassword(request.Password))
            {
                throw new BadRequestApiException("Password should contains one letter and one number," +
                    $" be in range {UserEntity.MIN_PASSWORD_LENGTH} to {UserEntity.MAX_PASSWORD_LENGTH}");
            }

            var role = await unitOfWork.RoleRepository.GetRole("User");

            if(role.IsFailure)
            {
                throw new AppConfigurationException("Roles doesnt configure");
            }

            var passwordHash = hashService
                .GenerateHash(request.Password);

            var userEntity = UserEntity.Initialize(
                request.Email,
                passwordHash,
                role.Value);

            if(userEntity.IsFailure)
            {
                throw new BadRequestApiException(userEntity.Error);
            }

            var newUser = await unitOfWork.UserRepository.AddUser(userEntity.Value);

            await unitOfWork.SaveChangesAsync();

            var token = jwtService.GenerateToken(userEntity.Value);

            return token;
        }
    }
}
