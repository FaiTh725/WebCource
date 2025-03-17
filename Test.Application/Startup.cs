using Microsoft.Extensions.DependencyInjection;
using Test.Application.Behaviors;
using Test.Application.Common.Mediator;
using Test.Application.Implementations;
using Test.Application.Interfaces;

namespace Test.Application
{
    public static class Startup
    {
        public static void ConfigureAppServices(
            this IServiceCollection services)
        {
            services.ConfigureMediatR();

            services.AddScoped<ITestEvaluationService, TestEvaluationService>();
            services.AddScoped<ITestAccessService, TestAccessService>();
            services.AddScoped<MediatorWrapper>();
        }

        private static IServiceCollection ConfigureMediatR(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
                cfg.AddOpenBehavior(typeof(AttemptOwnerAccessBehaviour<,>));
                cfg.AddOpenBehavior(typeof(TestAccessBehaviour<,>));
            });

            return services;
        }
    }
}
