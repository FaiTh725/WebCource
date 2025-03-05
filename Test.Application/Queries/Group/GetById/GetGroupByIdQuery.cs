using MediatR;
using Test.Application.Contracts.Group;

namespace Test.Application.Queries.Group.GetById
{
    public class GetGroupByIdQuery : IRequest<GroupResponse>
    {
        public long Id { get; set; }
    }
}
