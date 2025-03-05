using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces;
using Notification.Contracts.Email.Requests;
using Notification.Infastructure.Implementation;

namespace Notification.Infastructure
{
    public static class Startup
    {
        public static void ConfigureInfastructureServices(
            this IServiceCollection services)
        {
            services.AddSingleton<INotificationService<SendEmailRequest>, EmailNotificationService>();
        }
    }
}
