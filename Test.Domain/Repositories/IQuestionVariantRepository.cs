using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IQuestionVariantRepository
    {
        Task<TestVariant> AddQuestionVariant(TestVariant questionVariant);

        Task<TestVariant?> GetQuestionVariant(long id);

        Task DeleteQuestionVariant(long id);

        Task DeleteQuestionVariants(List<long> ids);
    }
}
