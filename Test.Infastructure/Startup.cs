using Application.Shared.Exceptions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Application.Contracts.Teacher;
using Test.Application.Interfaces;
using Test.Infastructure.Implementations;

namespace Test.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAzurite(configuration);

            services.AddSingleton<ITokenService<TeacherToken>, TokenService>();
            services.AddSingleton<IBlobService, BlobService>();
        }

        public static void AddAzurite(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var blobConnectionString = configuration
                .GetConnectionString("AzuriteStorage") ??
                throw new AppConfigurationException("Azurite connection string");

            services.AddSingleton(new BlobServiceClient(blobConnectionString));
        }
    }
}
