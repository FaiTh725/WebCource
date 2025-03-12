using Microsoft.Extensions.Hosting;
using Redis.OM;
using Test.Application.Contracts.TestAnswer;
using Test.Application.Contracts.TestAttempt;

namespace Test.Infastructure.BackgroundServices
{
    public class CreateRedisOmIndexes : BackgroundService
    {
        private readonly RedisConnectionProvider provider;

        public CreateRedisOmIndexes(
            RedisConnectionProvider provider)
        {
            this.provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await provider.Connection.CreateIndexAsync(typeof(AttemptRedisEntity));
            await provider.Connection.CreateIndexAsync(typeof(AnswerRedisEntity));
        }
    }
}
