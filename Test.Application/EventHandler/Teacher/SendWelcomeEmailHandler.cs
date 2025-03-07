using MassTransit;
using MediatR;
using Notification.Contracts.Email.Requests;
using Test.Domain.Event;

namespace Test.Application.EventHandler.Teacher
{
    public class SendWelcomeEmailHandler : INotificationHandler<TeacherRegisteredEvent>
    {
        private readonly IBus bus;

        public SendWelcomeEmailHandler(
            IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(TeacherRegisteredEvent notification, CancellationToken cancellationToken)
        {
            await bus.Publish(new SendEmailRequest
            {
                Consumer = notification.Email,
                Message = "Your teacher account registered yet",
                Subject = "Welcome"
            }, cancellationToken);
        }
    }
}
