using Test.Domain.Entities;
using Test.Domain.Primitives;

namespace Test.Domain.Repositories
{
    public interface ITestAttemptRepository
    {
        Task<TestAttempt> AddTestTestAttempt(TestAttempt testAttempt);

        IQueryable<TestAttempt> GetTestAttempts(Specification<TestAttempt> specification);
    }
}
