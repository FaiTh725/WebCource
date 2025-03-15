using Microsoft.EntityFrameworkCore;
using Test.Dal.Specifications;
using Test.Domain.Entities;
using Test.Domain.Primitives;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class TestAttemptRepository : ITestAttemptRepository
    {
        private readonly AppDbContext context;

        public TestAttemptRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TestAttempt> AddTestTestAttempt(TestAttempt testAttempt)
        {
            var testAttemptEntity = await context.Attempts
                .AddAsync(testAttempt);
        
            return testAttemptEntity.Entity;
        }

        public IQueryable<TestAttempt> GetTestAttempts(Specification<TestAttempt> specification)
        {

            return SpecificationEvaluator.GetQuery(
                context.Attempts
                    .AsNoTracking()
                    .AsSplitQuery(),
                specification);
        }
    }
}
