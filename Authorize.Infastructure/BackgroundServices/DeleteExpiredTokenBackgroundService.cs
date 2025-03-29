using Authorize.Application.Common.Hangfire;
using Authorize.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorize.Infastructure.BackgroundServices
{
    public class DeleteExpiredTokenBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DeleteExpiredTokenBackgroundService(
            IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = serviceScopeFactory.CreateAsyncScope();
            var backgroundService = scope.ServiceProvider
                .GetRequiredService<IBackgroundService>();

            backgroundService.CreateScheduleJob<HangFireWrapper>(x =>
                x.DeleteExpiredTokenWrapper(),
                "delete_expired_token",
                "0 0 * * * *");

            return Task.CompletedTask;
        }
    }
}
