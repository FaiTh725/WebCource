using Microsoft.EntityFrameworkCore;
using Test.Dal.Specifications;
using Test.Domain.Primitives;
using Test.Domain.Repositories;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Dal.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly AppDbContext context;

        public TestRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TestEntity> AddTest(TestEntity test)
        {
            var newTest = await context.Tests
                .AddAsync(test);

            return newTest.Entity;
        }

        public async Task<TestEntity?> GetTest(long id)
        {
            return await context.Tests.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<TestEntity> GetTests(Specification<TestEntity> specification)
        {
            return SpecificationEvaluator.GetQuery(
                context.Tests.AsNoTracking(),
                specification);
        }

        public async Task<TestEntity?> GetTestWithOwner(long id)
        {
            return await context.Tests
                .AsNoTracking()
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
