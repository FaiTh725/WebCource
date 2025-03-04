using MassTransit;

namespace Authorize.Application.Commands.User.Test
{
    public class RegisterUserStateData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool IsCreatedUser { get; set; }

        public bool IsCreatedStudent { get; set; }
    }
}
