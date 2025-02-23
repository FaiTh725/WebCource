using Authorize.Application;

namespace Authorize.API.Extentions
{
    public static class AppExtentions
    {
        public static void ConfigureMediats(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        }
    }
}
