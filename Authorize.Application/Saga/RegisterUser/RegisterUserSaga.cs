using Authorize.Application.Events;
using MassTransit;
using Notification.Contracts.Email.Requests;

namespace Authorize.Application.Saga.RegisterUser
{
    public class RegisterUserSaga : 
        MassTransitStateMachine<RegisterUserState>
    {
        public RegisterUserSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => UserRegistered, x => x.CorrelateById(y => y.Message.CorrelationId));
            Event(() => EmailSent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Initially(
                When(UserRegistered)
                .Then(context =>
                {
                    context.Saga.CorrelationId = context.Message.CorrelationId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.Message = context.Message.Message;
                    context.Saga.Subject = context.Message.Subject;
                })
                .PublishAsync(context => context.Init<SendEmailRequest>(new SendEmailRequest
                {
                    CorrelationId = context.Saga.CorrelationId,
                    Consumer = context.Saga.Email,
                    Subject = context.Saga.Subject,
                    Message = context.Message.Message
                }))
                .TransitionTo(SendingEmail));

            During(SendingEmail,
                When(EmailSent)
                .TransitionTo(Complete)
                .Finalize());

            SetCompletedWhenFinalized();
        }


        public Event<UserRegistered> UserRegistered { get; private set; }
        public Event<SendEmailRequest> EmailSent { get; private set; }

        public State SendingEmail { get; private set; }
        public State Complete { get; private set; }
    }
}
