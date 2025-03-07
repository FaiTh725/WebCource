using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Group;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Group.GetById
{
    public class GetGroupByIdHandler : IRequestHandler<GetGroupByIdQuery, GroupResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetGroupByIdHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<GroupResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await unitOfWork.GroupRepository
                .GetGroup(request.Id);

            if(group is null)
            {
                throw new NotFoundApiException("Group doesnt exist");
            }

            return new GroupResponse
            {
                Id = group.Id,
                GroupName = group.GroupName
            };
        }
    }
}
