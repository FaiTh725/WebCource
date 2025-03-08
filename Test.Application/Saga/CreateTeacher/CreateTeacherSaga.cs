using Authorize.Contracts.Events;
using Authorize.Contracts.User.Requests;
using MassTransit;
using Microsoft.Extensions.Logging;
using Test.Application.Events.Teacher;

namespace Test.Application.Saga.CreateTeacher
{
    public class CreateTeacherSaga : 
        MassTransitStateMachine<CreateTeacherState>
    {
        public CreateTeacherSaga(ILogger<CreateTeacherSaga> logger)
        {

            InstanceState(x => x.CurrentState);

            Event(() => StartCreateTeacher, x => x.CorrelateBy(m => m.Email, context => context.Message.Email).SelectId(ctx => NewId.NextGuid()));
            Event(() => FailCreateTeacher, x => x.CorrelateBy(m => m.Email, context => context.Message.Email));
            Event(() => SuccessCreateTeacher, x => x.CorrelateBy(m => m.Email, context => context.Message.Email));
            
            Event(() => FailChangeRole, x => x.CorrelateBy(m => m.Email, context => context.Message.Email));
            Event(() => SuccessChangeRole, x => x.CorrelateBy(m => m.Email, context => context.Message.Email));


            Initially(
                When(StartCreateTeacher)
                .Then(context =>
                {
                    logger.LogInformation("Start Creating Teacher");
                    context.Saga.Email = context.Message.Email;
                })
                .TransitionTo(CreatingTeacher));

            During(CreatingTeacher,
                When(SuccessCreateTeacher)
                .Then(context =>
                {
                    logger.LogInformation("Teacher Created");
                    context.Saga.Email = context.Message.Email;
                    context.Saga.Name = context.Message.Name;
                    context.Saga.GroupName = context.Message.GroupName;
                })
                .PublishAsync(context => context.Init<ChangeUserRoleRequest>(new ChangeUserRoleRequest
                {
                    Email = context.Saga.Email,
                    RoleName = "Teacher"
                }))
                .TransitionTo(ChangingRoleAccount),
                When(FailCreateTeacher)
                .Then(context =>
                {
                    logger.LogInformation("Create Teacher With Error");
                })
                .TransitionTo(Failed)
                .Finalize());

            During(ChangingRoleAccount,
                When(SuccessChangeRole)
                .Then(context =>
                {
                    logger.LogInformation("Role Changed");
                })
                .TransitionTo(Completed)
                .Finalize(),
                When(FailChangeRole)
                .Then(context =>
                {
                    logger.LogInformation("Role Changed With Error");
                })
                .PublishAsync(context => context.Init<RollBackCreatTeacher>(new RollBackCreatTeacher
                {
                    Email = context.Saga.Email,
                    Name = context.Saga.Name,
                    GroupName = context.Saga.GroupName
                }))
                .TransitionTo(Failed)
                .Finalize());

            SetCompletedWhenFinalized();
        }

        public State CreatingTeacher { get; private set; }
        public State ChangingRoleAccount { get; private set; }
        public State Failed { get; private set; }
        public State Completed { get; private set; }

        public Event<CreateTeacherRequest> StartCreateTeacher { get; private set; }
        public Event<FailCreateTeacher> FailCreateTeacher { get; private set; }
        public Event<SuccessCreateTeacher> SuccessCreateTeacher { get; private set; }
    
        public Event<SuccessChangeRole> SuccessChangeRole { get; private set; }
        public Event<FailChangeRole> FailChangeRole { get; private set; }
    }
}
