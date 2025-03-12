using Redis.OM.Modeling;

namespace Test.Application.Contracts.TestAnswer
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Answer" })]
    public class AnswerRedisEntity
    {
        [RedisIdField]
        public Guid AnswerId { get; set; }

        [Indexed]
        public Guid TestAttemptId { get; set; }

        [Indexed]
        public long TestAnswerId { get; set; }
    }
}
