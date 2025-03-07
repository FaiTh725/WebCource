using MassTransit;

namespace Test.Application.Saga.CreateTeacher
{
    public class CreateTeacherState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set ; }

        public string CurrentState { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
