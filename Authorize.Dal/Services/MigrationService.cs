using Authorize.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Authorize.Dal.Services
{
    public class MigrationService : IMigrationService
    {
        private readonly AppDbContext context;
        private readonly ILogger<MigrationService> logger;

        public MigrationService(
            AppDbContext context,
            ILogger<MigrationService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task ApplyMigrations()
        {
            try
            {
                var migrations = await GetPendingMigrations();

                if (migrations.Any())
                {
                    logger.LogInformation("Apply Migrations");
                    await context.Database.MigrateAsync();
                    return;
                }

                logger.LogInformation("Migration already applied");
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                logger.LogError("Apply Migrations with error");
            }
        }

        public async Task<IEnumerable<string>> GetPendingMigrations()
        {
            return await context.Database.GetPendingMigrationsAsync();
        }
    }
}
