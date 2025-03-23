using Authorize.Dal.Repositories;
using Authorize.Dal.Services;
using Authorize.Domain.Repositories;
using Authorize.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authorize.Dal
{
    public static class StartUp
    {
        public static void ConfigureAppRepositories(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMigrationService, MigrationService>();
        }
    }
}
