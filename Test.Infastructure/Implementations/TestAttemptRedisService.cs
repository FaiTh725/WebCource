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
            var attempt = await attempts
                .FirstOrDefaultAsync(x => x.AttemptId == id);

            return attempt;
        }

        public async Task<AttemptRedisEntity?> GetEntity(long tEntityMember)
        {
            var attempt = await attempts
                .FirstOrDefaultAsync(x => x.AnswerStudnetId == tEntityMember);

            return attempt;
        }

        public async Task RemoveEntity(Guid id)
        {
            await provider.Connection.UnlinkAsync($"Attempt:{id}");
        }
    }
}
