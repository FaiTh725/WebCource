using Authorize.Application.Implementations;
using Authorize.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Authorize.Application
{
    public static class Startup
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IHashService, HashService>();
        }
    }
}
