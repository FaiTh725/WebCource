using MediatR;
using Test.Application.Commands.Student.DeleteStudent;
using Test.Domain.Event;

namespace Test.Application.EventHandler.Teacher
{
    public class RemoveStudentHandler : INotificationHandler<TeacherRegisteredEvent>
    {
        private readonly IMediator mediator;

        public RemoveStudentHandler(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(TeacherRegisteredEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteStudentCommand
            {
                StudentEmail = notification.Email
            },
            cancellationToken);
        }
    }
}
