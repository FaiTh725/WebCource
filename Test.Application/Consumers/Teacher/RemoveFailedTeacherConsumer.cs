using MassTransit;
using MediatR;
using Test.Application.Commands.Student.CreateStudent;
using Test.Application.Commands.Teacher.DeleteTeacher;
using Test.Application.Events.Teacher;

namespace Test.Application.Consumers.Teacher
{
    public class RemoveFailedTeacherConsumer : IConsumer<RollBackCreatTeacher>
    {
        private readonly IMediator mediator;

        public RemoveFailedTeacherConsumer(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RollBackCreatTeacher> context)
        {
            await mediator.Send(new DeleteTeacherCommand
            {
                TeacherEmail = context.Message.Email
            });

            await mediator.Send(new CreateStudentCommand
            {
                Email = context.Message.Email,
                Group = context.Message.GroupName,
                Name = context.Message.Name
            });
        }
    }
}
