using MediatR;
using Test.Application.Contracts.Test;
using Test.Application.Interfaces;

namespace Test.Application.Queries.Test.GetTestByIdOwner
{
    public class GetTestByIdOwnerQuery : 
        IRequest<TestOwnerResponse>,
        ICachQuery
    {
        public long Id { get; set; }

        public string Key => "TestsWithOwner:" + Id;

        public int ExpirationSecond => 120;
    }
}
