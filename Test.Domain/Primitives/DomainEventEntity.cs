using CSharpFunctionalExtensions;

namespace Test.Domain.Primitives
{
    public class DomainEventEntity : Entity, IEntity
    {
        private readonly List<IDomainEvent> domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents.ToList();

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }
    }
}
