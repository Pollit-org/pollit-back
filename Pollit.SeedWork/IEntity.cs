using Pollit.SeedWork.Eventing;

namespace Pollit.SeedWork;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}