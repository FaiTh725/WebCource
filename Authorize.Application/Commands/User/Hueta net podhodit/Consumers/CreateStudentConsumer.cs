using Authorize.Application.Commands.User.Test.Commands;
using Authorize.Application.Commands.User.Test.Events;
using MassTransit;

namespace Authorize.Application.Commands.User.Test.Consumers
{
    public class CreateStudentConsumer : IConsumer<CreateStudentCommand>
    {
        public async Task Consume(ConsumeContext<CreateStudentCommand> context)
        {
            Console.WriteLine("Creating Student");

            await Task.Delay(500);

            await context.Publish(new StudentCreatedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                Email = context.Message.Email,
                StudentId = -1
            });
        }
    }
}
