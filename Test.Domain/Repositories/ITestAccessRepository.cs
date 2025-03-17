using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface ITestAccessRepository
    {

        Task<TestAccess> AddTestAccess(TestAccess testAccess);

        Task<TestAccess?> GetTestAccess(long testId, long studentId);

        Task DeleteTestAccess(long testId, long studentId);
    }
}
