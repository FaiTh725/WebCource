using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Domain.Services;

namespace Test.Dal.Services
{
    public class MigrationsService : IMigrationService
    {
        private readonly AppDbContext context;
        private readonly ILogger<MigrationsService> logger;

        public MigrationsService(
            AppDbContext context,
            ILogger<MigrationsService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task ApplyMigrations()
        {
            try
            {
                var migrations = await GetPendingMigrations();

                if(migrations.Any())
                {
                    logger.LogInformation("apply Migrations");
                    await context.Database.MigrateAsync();
                    return;
                }

                logger.LogInformation("Migrations already applied");
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                logger.LogError("Apply migrations with error");
            }
        }

        public async Task<IEnumerable<string>> GetPendingMigrations()
        {
            return await context.Database.GetPendingMigrationsAsync();
        }
    }
}
