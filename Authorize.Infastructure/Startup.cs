using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Infastructure.BackgroundServices;
using Authorize.Infastructure.Implementations;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Authorize.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddHangfireProvider(configuration);

            services.AddHostedService<ApplyMigrationsBackgroundService>();
            services.AddHostedService<InitializeRoles>();
            services.AddHostedService<DeleteExpiredTokenBackgroundService>();

            services.AddScoped<IBackgroundService, BackgroundService>();
            services.AddSingleton<IJwtService<GenerateUserToken, TokenResponse>, JwtUserService>();
        }

        private static IServiceCollection AddHangfireProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var sqlServerConnectionString = configuration
                .GetConnectionString("SQLServerConnection") ??
                throw new AppConfigurationException("SQLServer connection string");

            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            services.AddHangfire(x =>
            {
                x.UseSimpleAssemblyNameTypeSerializer()
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSqlServerStorage(sqlServerConnectionString)
                .UseSerializerSettings(jsonSettings);
            });

            services.AddHangfireServer();

            return services;
        }
    }
}
