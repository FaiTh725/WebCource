using Authorize.Application.Commands.User.ChangeRole;
using Authorize.Contracts.Events;
using Authorize.Contracts.User.Requests;
using MassTransit;
using MediatR;

namespace Authorize.Application.Consumers.User
{
    public class ChangeUserRoleConsumer : IConsumer<ChangeUserRoleRequest>
    {
        private readonly IBus bus;
        private readonly IMediator mediator;

        public ChangeUserRoleConsumer(
            IBus bus,
            IMediator mediator)
        {
            this.bus = bus;
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ChangeUserRoleRequest> context)
        {
            try
            {
                await mediator.Send(new ChangeRoleCommand 
                { 
                    UserEmail = context.Message.Email,
                    RoleName = context.Message.RoleName
                });

                await bus.Publish(new SuccessChangeRole
                {
                    Email = context.Message.Email
                });
            }
            catch(Exception ex)
            {
                await bus.Publish(new FailChangeRole
                {
                    Reason = ex.Message,
                    Email = context.Message.Email
                });
            }
        }
    }
}
