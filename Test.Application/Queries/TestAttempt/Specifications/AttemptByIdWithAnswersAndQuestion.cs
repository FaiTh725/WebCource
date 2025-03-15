using Test.Domain.Primitives;
using TestAttemptEntity = Test.Domain.Entities.TestAttempt;


namespace Test.Application.Queries.TestAttempt.Specifications
{
    public class AttemptByIdWithAnswersAndQuestion : Specification<TestAttemptEntity>
    {
        public AttemptByIdWithAnswersAndQuestion(
            long id)
        {
            AddCreteria(x => x.Id == id);

            AddInclude(x => x.Answers);
            AddInclude("Answers.Question");
            AddInclude(x => x.Answers);
            AddInclude("Answers.Answers");
        }
    }
}
