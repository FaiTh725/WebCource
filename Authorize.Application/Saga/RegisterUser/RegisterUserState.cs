using MassTransit;

namespace Authorize.Application.Saga.RegisterUser
{
    public class RegisterUserState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set ; }

        public string CurrentState { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
