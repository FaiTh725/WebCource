using MassTransit;
using MediatR;
using Test.Application.Commands.Student.CreateStudent;
using Test.Contracts.Student;

namespace Test.Application.Consumers.Student
{
    public class StudentCreatedConsumer : IConsumer<StudentCreated>
    {
        private readonly IMediator mediator;

        public StudentCreatedConsumer(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<StudentCreated> context)
        {
            var studentId = await mediator.Send(new CreateStudentCommand
            {
                Group = context.Message.GroupNumber,
                Email = context.Message.Email,
                Name = context.Message.Name
            });
        }
    }
}
