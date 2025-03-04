using Application.Shared.Exceptions;
using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using MassTransit;
using MediatR;
using Test.Contracts.Student.Requests;
using Test.Contracts.Student.Responses;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.Application.Commands.User.Register
{
    // implement token in another api
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, long>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;
        private readonly IRequestClient<CreateStudentRequest> client;

        public RegisterUserHandler(
            IUnitOfWork unitOfWork,
            IHashService hashService,
            IRequestClient<CreateStudentRequest> client)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.client = client;
        }

        public async Task<long> Handle(
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

            await unitOfWork.BeginTransactionAsync();

            var newUser = await unitOfWork.UserRepository.AddUser(userEntity.Value);

            var responseCreateStudent = await client.GetResponse<StudentCreatedResponse>(new CreateStudentRequest
            {
                Email = request.Email,
                Name = request.Name,
                GroupNumber = request.Group
            });

            if(!responseCreateStudent.Message.IsSuccess)
            {
                await unitOfWork.RollBackTransactionAsync();
                throw new BadRequestApiException(responseCreateStudent.Message.ErrorMessage);
            }
            else
            {
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();
            }

            return newUser.Id;
        }
    }
}
