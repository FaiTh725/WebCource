using MediatR;
using Test.Application.Contracts.Test;

namespace Test.Application.Queries.Test.GetTestByIdOwner
{
    public class GetTestByIdOwnerQuery : IRequest<TestOwnerResponse>
    {
        public long Id { get; set; }
    }
}
