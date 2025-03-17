using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class TestAccessRepository : ITestAccessRepository
    {
        private readonly AppDbContext context;

        public TestAccessRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TestAccess> AddTestAccess(TestAccess testAccess)
        {
            var testAccessEntity = await context.TestAccesses
                .AddAsync(testAccess);

            return testAccessEntity.Entity;
        }

        public async Task DeleteTestAccess(long testId, long studentId)
        {
            await context.TestAccesses
                .Where(x => x.TestId == testId && x.StudentId == studentId)
                .ExecuteDeleteAsync();
        }

        public async Task<TestAccess?> GetTestAccess(long testId, long studentId)
        {
            return await context.TestAccesses
                .FirstOrDefaultAsync(x => x.TestId == testId && x.StudentId == studentId);
        }
    }
}
