using Authorize.Application.Contacts.Token;
using Authorize.Application.Interfaces;
using Authorize.Domain.Entities;
using Authorize.Infastructure.BackgroundServices;
using Authorize.Infastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Authorize.Infastructure
{
    public static class Startup
    {
        public static void ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<InitializeRoles>();
        }

        public static void ConfigureInfastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtService<User, TokenResponse>, JwtUserService>();
        }
    }
}
