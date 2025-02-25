
using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Queries.Group.Responses;
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

            if(group.IsFailure)
            {
                throw new NotFoundApiException(group.Error);
            }

            return new GroupResponse
            {
                Id = group.Value.Id,
                GroupName = group.Value.GroupName
            };
        }
    }
}
