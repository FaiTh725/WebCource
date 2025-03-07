using Authorize.Domain.Events;
using MassTransit;
using MediatR;
using Notification.Contracts.Email.Requests;

namespace Authorize.Application.EventHandlers.User
{
    public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly IBus bus;

        public SendWelcomeEmailHandler(
            IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            await bus.Publish(new SendEmailRequest
            {
                Consumer = notification.UserEmail,
                Message = "Thank you for registering on the FaITh Tests website",
                Subject = "Welcome"
            }, cancellationToken);
        }
    }
}
