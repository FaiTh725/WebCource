using Authorize.Domain.Primitives;

namespace Authorize.Domain.Events
{
    public class UserRegisteredEvent : IDomainEvent
    {
        public required string UserEmail { get; set; }
    }
}
