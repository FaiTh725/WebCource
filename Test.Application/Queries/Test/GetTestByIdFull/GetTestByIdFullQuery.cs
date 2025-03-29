using MediatR;
using Test.Application.Contracts.Test;
using Test.Application.Interfaces;

namespace Test.Application.Queries.Test.GetTestByIdFull
{
    public class GetTestByIdFullQuery : 
        IRequest<TestFullResponse>, 
        ICachQuery
    {
        public long TestId { get; set; }

        public string Key => "FullTests:" + TestId;

        public int ExpirationSecond => 120;
    }
}
