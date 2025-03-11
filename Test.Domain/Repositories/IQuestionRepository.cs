using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<TestQuestion> AddQuestion(TestQuestion testQuestion);

        Task<TestQuestion?> GetQuestion(long id);

        Task<TestQuestion?> GetQuestionWithVariants(long id);

        Task DeleteQuestion(long id);
    }
}
