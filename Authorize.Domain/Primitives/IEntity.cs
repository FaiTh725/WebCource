namespace Authorize.Domain.Primitives
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
