using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MassTransit;
using MediatR;
using Test.Contracts.Student;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Commands.User.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, string>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;
        private readonly IJwtService<UserEntity, TokenResponse> jwtService;
        private readonly IBus bus;

        public RegisterUserHandler(
            IUnitOfWork unitOfWork,
            IHashService hashService,
            IJwtService<UserEntity, TokenResponse> jwtService,
            IBus bus)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.jwtService = jwtService;
            this.bus = bus;
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

            await bus.Publish(new StudentCreated
            {
                Email = request.Email,
                Name = request.Name,
                GroupNumber = request.Group
            });

            await unitOfWork.SaveChangesAsync();

            var token = jwtService.GenerateToken(userEntity.Value);

            return token;
        }
    }
}
