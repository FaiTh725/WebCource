using MassTransit;
using MediatR;
using Test.Domain.Event;

namespace Test.Application.EventHandler.Teacher
{
    public class ChangeUserRoleHandler : INotificationHandler<TeacherRegisteredEvent>
    {
        private readonly IBus bus;

        public ChangeUserRoleHandler(IBus bus)
        {
            this.bus = bus;
        }

        public Task Handle(TeacherRegisteredEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
