using Authorize.Application.Commands.User.Test.Commands;
using Authorize.Application.Commands.User.Test.Events;
using MassTransit;

namespace Authorize.Application.Commands.User.Test
{
    public class RegisterUserSaga : 
        MassTransitStateMachine<RegisterUserStateData>
    {
        public RegisterUserSaga()
        {
            InstanceState(x => x.CurrentState);

            SetCompletedWhenFinalized(); // delete

            Event(() => RegisterUser, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => UserCreated, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => StudentCreated, x => x.CorrelateById(context => context.Message.CorrelationId));

            Event(() => UserCreationFailed, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => StudentCreationFailed, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(RegisterUser)
                .Then(context =>
                {
                    Console.WriteLine($"Start RegisterUser");
                })
                .PublishAsync(context => context.Init<CreateUserCommand>(new CreateUserCommand()
                {
                    CorrelationId = context.Message.CorrelationId,
                    Email = context.Message.Email,
                    Password = context.Message.Password,
                    Group = context.Message.Group,
                    Name = context.Message.Name
                }))
                .TransitionTo(WaitingForUser));

            During(WaitingForUser,
                When(UserCreated)
                    .Then(context =>
                    {
                        Console.WriteLine("User Created");
                        context.Saga.Email = context.Message.Email;
                        context.Saga.CorrelationId = context.Message.CorrelationId;
                    })
                    .PublishAsync(context => context.Init<CreateStudentCommand>(new CreateStudentCommand
                    {
                        CorrelationId = context.Message.CorrelationId,
                        Email = context.Message.Email,
                        Group = context.Message.Group,
                        Name = context.Message.Name
                    }))
                    .TransitionTo(WaitingForStudent),
                When(UserCreationFailed)
                .Then(context =>
                {
                    Console.WriteLine("User Creating Failed");

                })
                .Finalize());

            During(WaitingForStudent, 
                When(StudentCreated)
                    .Then(context =>
                    {
                        Console.WriteLine("StudentCreated");
                        context.Saga.Email = context.Message.Email;
                        context.Saga.CorrelationId = context.Message.CorrelationId;
                    })
                    .Finalize(),
                When(StudentCreationFailed)
                .Then(context =>
                {
                    Console.WriteLine("StudentCreated");
                })
                .Finalize());
        }

        // События
        public Event<RegisterUserCommand> RegisterUser { get; private set; }
        public Event<UserCreatedEvent> UserCreated { get; private set; }
        public Event<StudentCreatedEvent> StudentCreated { get; private set; }
        public Event<UserCreatedFailedEvent> UserCreationFailed { get; private set; }
        public Event<StudentCreatedFailedEvent> StudentCreationFailed { get; private set; }

        // Состояния
        public State WaitingForUser { get; private set; }
        public State WaitingForStudent { get; private set; }
    }
}
