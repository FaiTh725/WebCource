using TestAnswerEntity = Test.Domain.Entities.TestAnswer;

namespace Test.Application.Contracts.Test
{
    public class SolvedTestResult
    {
        public double Percent {  get; set; }

        public List<TestAnswerEntity> StudentAnswers { get; set; } = new List<TestAnswerEntity>();
    }
}
