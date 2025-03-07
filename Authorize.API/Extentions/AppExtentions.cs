using Application.Shared.Exceptions;
using Authorize.API.Helpers.Conf;
using Authorize.Application;
using Authorize.Application.Consumers.User;
using Authorize.Application.Saga.RegisterUser;
using MassTransit;

namespace Authorize.API.Extentions
{
    public static class AppExtentions
    {
        public static void ConfigureMediats(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
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

                conf.AddConsumer<ChangeUserRoleConsumer>();

                //conf.AddSagaStateMachine<RegisterUserSaga, RegisterUserState>()
                //.InMemoryRepository();

                //conf.AddConsumer<CreateUserConsumer>();
                //conf.AddConsumer<CreateStudentConsumer>();

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
    }
}
