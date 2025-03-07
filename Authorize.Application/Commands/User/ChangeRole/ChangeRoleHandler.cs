using Application.Shared.Exceptions;
using Application.Shared.Results;
using Authorize.Domain.Repositories;
using MassTransit;
using MediatR;
using UserEntity = Authorize.Domain.Entities.User;


namespace Authorize.Application.Commands.User.ChangeRole
{
    public class ChangeRoleHandler : IRequestHandler<ChangeRoleCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public ChangeRoleHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository
                .GetRole(request.RoleName);

            if (role is null)
            {
                throw new BadRequestApiException("Curren role doesnt exist");
            }

            var user = await unitOfWork.UserRepository
                .GetUser(request.UserEmail);

            if (user is null)
            {
                throw new NotFoundApiException("User doesnt registered");
            }

            var userUpdate = UserEntity.Initialize(
                user.Email,
                user.PasswordHash,
                role);

            if (userUpdate.IsFailure)
            {
                throw new InternalServerApiException("Error initialize user");
            }

            user.SetRole(role);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
