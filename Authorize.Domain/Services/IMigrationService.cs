namespace Authorize.Domain.Services
{
    public interface IMigrationService
    {
        Task<IEnumerable<string>> GetPendingMigrations();

        Task ApplyMigrations();
    }
}
