using Application.Shared.Exceptions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;
using Test.Application.Contracts.Teacher;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Infastructure.BackgroundServices;
using Test.Infastructure.Implementations;

namespace Test.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAzurite(configuration);
            services.AddRedisProvider(configuration);

            services.AddScoped<IRedisEntityService<AttemptRedisEntity>, TestAttemptRedisService>();
            services.AddSingleton<ITokenService<TeacherToken>, TokenService>();
            services.AddSingleton<IBlobService, BlobService>();

            services.AddHostedService<CreateRedisOmIndexes>();
        }

        private static void AddAzurite(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var blobConnectionString = configuration
                .GetConnectionString("AzuriteStorage") ??
                throw new AppConfigurationException("Azurite connection string");

            services.AddSingleton(new BlobServiceClient(blobConnectionString));
        }

        private static void AddRedisProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("RedisConnection") ??
                throw new AppConfigurationException("Redis connection string");

            services.AddSingleton(new RedisConnectionProvider(connectionString));
        }
    }
}
