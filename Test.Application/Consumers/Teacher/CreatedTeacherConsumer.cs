using MassTransit;
using MediatR;
using Test.Application.Commands.Teacher.CreateTeacher;
using Test.Application.Events.Teacher;
using Test.Application.Queries.Student.GetStudentByEmail;

namespace Test.Application.Consumers.Teacher
{
    public class CreatedTeacherConsumer : IConsumer<CreateTeacherRequest>
    {
        private readonly IMediator mediator;
        private readonly IBus bus;

        public CreatedTeacherConsumer(
            IMediator mediator,
            IBus bus)
        {
            this.mediator = mediator;
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<CreateTeacherRequest> context)
        {
            try
            {
                var existStudent = await mediator.Send(new GetStudentByEmailQuery
                {
                    Email = context.Message.Email
                });

                await mediator.Send(new CreateTeacherCommand
                {
                    Email = context.Message.Email
                });

                await bus.Publish(new SuccessCreateTeacher
                {
                    Email = context.Message.Email,
                    Name = existStudent.Name,
                    GroupName = existStudent.GroupName
                });
            }
            catch(Exception ex)
            {
                await bus.Publish(new FailCreateTeacher
                {
                    Email = context.Message.Email,
                    Reason = ex.Message
                });
            }
        }
    }
}
