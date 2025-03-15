using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext context;

        public QuestionRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TestQuestion> AddQuestion(TestQuestion testQuestion)
        {
            var question = await context.Questions
                .AddAsync(testQuestion);

            return question.Entity;
        }

        public async Task DeleteQuestion(long id)
        {
            await context.Questions
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<TestQuestion?> GetQuestion(long id)
        {
            return await context.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TestQuestion?> GetQuestionWithVariants(long id)
        {
            return await context.Questions
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Variants)
                .FirstOrDefaultAsync();
        }
    }
}
