
using Test.Domain.Primitives;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Application.Queries.Test.Specifications
{
    public class TestByIdWithQuestionsAndAnswersSpecification : 
        Specification<TestEntity>
    {
        public TestByIdWithQuestionsAndAnswersSpecification(
            long id)
        {
            AddCreteria(x => x.Id == id);

            AddInclude(x => x.Questions);
            AddInclude("Questions.Variants");
        }
    }
}
