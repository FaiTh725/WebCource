using Test.Domain.Primitives;

namespace Test.Domain.Event
{
    public class TeacherRegisteredEvent : IDomainEvent
    {
        public required string Email { get; set; }
    }
}
