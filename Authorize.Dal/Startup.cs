using Authorize.Dal.Repositories;
using Authorize.Domain.Repositories;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
