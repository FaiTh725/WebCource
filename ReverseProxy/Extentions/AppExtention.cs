namespace ReverseProxy.Extentions
{
    public static class AppExtention
    {
        public static IServiceCollection ConfigureReverseProxy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));

            return services;
        }
    }
}
