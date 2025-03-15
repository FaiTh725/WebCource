using Redis.OM;
using Redis.OM.Searching;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;

namespace Test.Infastructure.Implementations
{
    public class TestAttemptRedisService : IRedisEntityService<AttemptRedisEntity>
    {
        private readonly RedisCollection<AttemptRedisEntity> attempts;
        private readonly RedisConnectionProvider provider;

        public TestAttemptRedisService(
            RedisConnectionProvider provider)
        {
            this.provider = provider;
            attempts = (RedisCollection<AttemptRedisEntity>)provider.RedisCollection<AttemptRedisEntity>();
        }

        public async Task<AttemptRedisEntity> AddEntity(AttemptRedisEntity entity)
        {
            await attempts.InsertAsync(entity);

            return entity;
        }

        public async Task<AttemptRedisEntity?> GetEntity(Guid id)
        {
            var attemptsList = await attempts.ToListAsync();
            var attempt = attemptsList
                .FirstOrDefault(x => x.AttemptId == id);

            return attempt;
        }

        public async Task<AttemptRedisEntity?> GetEntity(long tEntityMember)
        {
            var attempt = await attempts
                .FirstOrDefaultAsync(x => x.AnswerStudentId == tEntityMember);

            return attempt;
        }

        public async Task RemoveEntity(Guid id)
        {
            await provider.Connection.UnlinkAsync($"Attempt:{id}");
        }

        public async Task UpdateEntity(AttemptRedisEntity entity)
        {
            var attempt = await GetEntity(entity.AttemptId);

            if (attempt is null)
            {
                return;
            }

            attempt.TestTime = entity.TestTime;

            attempt.Answers.Clear();
            attempt.Answers.AddRange(entity.Answers);

            await attempts.SaveAsync();
        }
    }
}
