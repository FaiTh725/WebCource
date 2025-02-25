using Application.Shared.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test.API.Helpers.Conf;
using Test.Application;
using Test.Application.Consumers.Student;

namespace Test.API.Extentions
{
    public static class AppExtention
    {
        
        public static void AddJwtAuth(
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
        }

        public static void AddRabbitMq(
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

                conf.AddConsumer<StudentCreatedConsumer>();

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
        }

        public static void ConfigureMediatr(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        }
    }
}
