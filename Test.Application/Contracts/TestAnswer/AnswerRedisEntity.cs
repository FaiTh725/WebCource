using Redis.OM.Modeling;

namespace Test.Application.Contracts.TestAnswer
{
    public class AnswerRedisEntity
    {
        public List<long> TestAnswersId { get; set; } = new List<long>();

        public long QuestionId { get; set; }

        public DateTime SendTime { get; set; }
    }
}
