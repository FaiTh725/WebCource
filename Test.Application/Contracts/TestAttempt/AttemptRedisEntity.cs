
using Redis.OM.Modeling;
using Test.Application.Contracts.TestAnswer;

namespace Test.Application.Contracts.TestAttempt
{
    [Document(StorageType = StorageType.Json, Prefixes = new [] {"Attempt"})]
    public class AttemptRedisEntity
    {
        [RedisIdField]
        [Indexed]
        public Guid AttemptId { get; set; }

        [Indexed]
        public long AnswerStudentId { get; set; }

        [Indexed]
        public long TestId { get; set; }

        [Indexed]
        public DateTime StartDate { get; set; }

        [Indexed(CascadeDepth = 1)]
        public List<AnswerRedisEntity> Answers { get; set; } = new List<AnswerRedisEntity>();
        
        public int TestTime { get; set; }
    }
}
