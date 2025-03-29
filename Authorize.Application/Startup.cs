using Authorize.Application.Common.Hangfire;
using Authorize.Application.Implementations;
using Authorize.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Authorize.Application
{
    public static class Startup
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.ConfigureMediats();

            services.AddSingleton<IHashService, HashService>();
            services.AddScoped<HangFireWrapper>();
        }

        private static IServiceCollection ConfigureMediats(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
               cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            return services;
        }
    }
}
