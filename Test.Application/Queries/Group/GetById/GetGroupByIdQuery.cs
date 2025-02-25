
using MediatR;
using Test.Application.Queries.Group.Responses;

namespace Test.Application.Queries.Group.GetById
{
    public class GetGroupByIdQuery : IRequest<GroupResponse>
    {
        public long Id { get; set; }
    }
}
