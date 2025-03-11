using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class QuestionVariantRepository : IQuestionVariantRepository
    {
        private readonly AppDbContext context;

        public QuestionVariantRepository(
            AppDbContext context)
        {
            this.context = context;    
        }

        public async Task<TestVariant> AddQuestionVariant(TestVariant questionVariant)
        {
            var question = await context.QuestionAnswers
                .AddAsync(questionVariant);

            return question.Entity;
        }

        public async Task DeleteQuestionVariant(long id)
        {
            await context.QuestionAnswers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteQuestionVariants(List<long> ids)
        {
            if (ids is null || ids.Count == 0)
            {
                return;
            }

            await context.QuestionAnswers
                .Where(x => ids.Contains(x.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<TestVariant?> GetQuestionVariant(long id)
        {
            return await context.QuestionAnswers
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
