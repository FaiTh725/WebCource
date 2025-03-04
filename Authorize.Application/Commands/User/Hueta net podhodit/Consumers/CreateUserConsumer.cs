using Authorize.Application.Commands.User.Test.Commands;
using Authorize.Application.Commands.User.Test.Events;
using MassTransit;

namespace Authorize.Application.Commands.User.Test.Consumers
{
    public class CreateUserConsumer : IConsumer<CreateUserCommand>
    {
        public async Task Consume(ConsumeContext<CreateUserCommand> context)
        {
            Console.WriteLine("Creating User");
            await Task.Delay(500);

            await context.Publish(new UserCreatedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                Email = context.Message.Email,
                Group = context.Message.Group,
                Name = context.Message.Name,
                UserId = -1
            });
        }
    }
}
