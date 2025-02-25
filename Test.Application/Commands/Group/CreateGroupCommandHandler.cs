using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Group
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateGroupCommandHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await unitOfWork.GroupRepository
                .GetGroup(request.Name);

            if (group.IsSuccess)
            {
                throw new ConflictApiException("Group already exist");
            }

            var groupEntity = StudentGroup.Initialize(request.Name);

            if (groupEntity.IsFailure)
            {
                throw new BadRequestApiException("Error with request - " +
                    groupEntity.Error);
            }

            var newGroup = await unitOfWork.GroupRepository
                .AddGroup(groupEntity.Value);

            await unitOfWork.SaveChangesAsync();

            return newGroup.Id;
        }
    }
}
