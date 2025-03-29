using Authorize.API.Validators.User;
using Authorize.Application.Commands.User.Login;
using Authorize.Application.Commands.User.Register;
using FluentValidation;

namespace Authorize.API.Extentions
{
    public static class AppExtentions
    {

        public static IServiceCollection ConfigureValidators(
            this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserRequest>, LoginUserValidator>();

            return services;
        }
    }
}
