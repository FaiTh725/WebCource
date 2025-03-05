using MassTransit;
using Notification.Application.Interfaces;
using Notification.Contracts.Email.Requests;

namespace Notification.Application.Consumers.Email
{
    public class SentEmailConsumer : IConsumer<SendEmailRequest>
    {
        private readonly INotificationService<SendEmailRequest> notificationService;

        public SentEmailConsumer(
            INotificationService<SendEmailRequest> notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task Consume(ConsumeContext<SendEmailRequest> context)
        {
            await notificationService.SendNotification(context.Message);
        }
    }
}
