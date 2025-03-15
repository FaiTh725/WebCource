using Test.Application.Contracts.TestAnswer;

namespace Test.Application.Contracts.TestAttempt
{
    public class TestAttemptWithAnswersResponse
    {
        public long Id { get; set; }

        public double Percent { get; set; }

        public DateTime PassedTime { get; set; }

        public List<TestAnswerResponse> Answers { get; set; } = new ();
    }
}
