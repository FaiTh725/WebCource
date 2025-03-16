using Test.Domain.Entities;
using Test.Domain.Primitives;

namespace Test.Domain.Repositories
{
    public interface ITestAttemptRepository
    {
        Task<TestAttempt> AddTestAttempt(TestAttempt testAttempt);

        IQueryable<TestAttempt> GetTestAttempts(Specification<TestAttempt> specification);

        Task<TestAttempt?> GetTestAttemptWithOwner(long id);
    }
}
