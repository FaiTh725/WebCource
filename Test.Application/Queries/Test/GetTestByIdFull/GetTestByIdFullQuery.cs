using MediatR;
using Test.Application.Contracts.Test;

namespace Test.Application.Queries.Test.GetTestByIdFull
{
    public class GetTestByIdFullQuery : IRequest<TestFullResponse>
    {
        public long TestId { get; set; }
    }
}
