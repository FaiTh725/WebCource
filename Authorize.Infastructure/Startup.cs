using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Infastructure.BackgroundServices;
using Authorize.Infastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Authorize.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(this IServiceCollection services)
        {
            services.AddHostedService<InitializeRoles>();

            services.AddSingleton<IJwtService<GenerateUserToken, TokenResponse>, JwtUserService>();
        }
    }
}
