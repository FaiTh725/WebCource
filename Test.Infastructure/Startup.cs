using Application.Shared.Exceptions;
using Azure.Storage.Blobs;
using Hangfire;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Redis.OM;
using System.Text;
using Test.Application.Consumers.Student;
using Test.Application.Consumers.Teacher;
using Test.Application.Contracts.Teacher;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Application.Saga.CreateTeacher;
using Test.Infastructure.BackgroundServices;
using Test.Infastructure.Configurations;
using Test.Infastructure.Implementations;

namespace Test.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddAzurite(configuration)
                .AddRedisProvider(configuration)
                .AddHangfireProvider(configuration)
                .ConfigureMassTransit(configuration)
                .AddJwtAuth(configuration);

            services.AddScoped<IRedisEntityService<AttemptRedisEntity>, TestAttemptRedisService>();
            services.AddScoped<IBackgroundJobService, BackgroundJobService>();
            services.AddSingleton<ITokenService<TeacherToken>, TokenService>();
            services.AddSingleton<IBlobService, BlobService>();

            services.AddHostedService<CreateRedisOmIndexes>();
        }

        private static IServiceCollection AddAzurite(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var blobConnectionString = configuration
                .GetConnectionString("AzuriteStorage") ??
                throw new AppConfigurationException("Azurite connection string");

            services.AddSingleton(new BlobServiceClient(blobConnectionString));

            return services;
        }

        private static IServiceCollection AddRedisProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("RedisConnection") ??
                throw new AppConfigurationException("Redis connection string");

            services.AddSingleton(new RedisConnectionProvider(connectionString));
            
            return services;
        }

        private static IServiceCollection AddHangfireProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var sqlServerConnectionString = configuration
                .GetConnectionString("SQLServerConnection") ??
                throw new AppConfigurationException("SQlServer connection string");

            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
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

        private static IServiceCollection ConfigureMassTransit(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var rabbitMqConf = configuration
                .GetSection("RabbitMq")
                .Get<RabbitMqConf>() ??
                throw new AppConfigurationException("RabbitMq Configuration Section");

            services.AddMassTransit(conf =>
            {
                conf.SetKebabCaseEndpointNameFormatter();

                conf.AddSagaStateMachine<CreateTeacherSaga, CreateTeacherState>()
                .InMemoryRepository();

                conf.AddConsumer<StudentCreatedConsumer>();
                conf.AddConsumer<CreatedTeacherConsumer>();

                conf.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqConf.Host, h =>
                    {
                        h.Username(rabbitMqConf.UserLogin);
                        h.Password(rabbitMqConf.UserPassword);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        private static IServiceCollection AddJwtAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtConf = configuration
                .GetSection("JwtSetting")
                .Get<JwtConf>() ??
                throw new AppConfigurationException("Jwt Token Setting");

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtConf.Audience,
                        ValidIssuer = jwtConf.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtConf.SecretKey)),
                    };

                    jwtOptions.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                        {
                            var token = ctx.Request.Cookies["token"];
                            if (!string.IsNullOrEmpty(token))
                            {
                                ctx.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
