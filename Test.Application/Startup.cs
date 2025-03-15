using Microsoft.Extensions.DependencyInjection;
using Test.Application.Implementations;
using Test.Application.Interfaces;

namespace Test.Application
{
    public static class Startup
    {
        public static void ConfigureAppServices(
            this IServiceCollection services)
        {
            services.AddScoped<ITestEvaluationService, TestEvaluationService>();
        }
    }
}
