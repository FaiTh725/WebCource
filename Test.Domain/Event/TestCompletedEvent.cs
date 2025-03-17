using Test.Domain.Primitives;

namespace Test.Domain.Event
{
    public class TestCompletedEvent : IDomainEvent
    {
        public long TestId { get; set; }

        public long StudentId { get; set; }
    }
}
