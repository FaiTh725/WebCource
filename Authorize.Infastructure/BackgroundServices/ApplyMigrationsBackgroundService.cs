using Authorize.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Authorize.Infastructure.BackgroundServices
{
    public class ApplyMigrationsBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ApplyMigrationsBackgroundService> logger;

        public ApplyMigrationsBackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ApplyMigrationsBackgroundService> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Start database migrations");

            using var scope = serviceScopeFactory.CreateAsyncScope();
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();

            await migrationService.ApplyMigrations();
        }
    }
}
