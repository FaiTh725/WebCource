using Test.Domain.Primitives;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Domain.Repositories
{
    public interface ITestRepository
    {
        Task<TestEntity> AddTest(TestEntity test);

        Task<TestEntity?> GetTest(long id);

        Task<TestEntity?> GetTestWithOwner(long id);

        IQueryable<TestEntity> GetTests(Specification<TestEntity> specification); 
    }
}
