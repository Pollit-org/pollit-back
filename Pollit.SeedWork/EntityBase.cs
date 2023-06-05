using Pollit.SeedWork.Eventing;

namespace Pollit.SeedWork;

public abstract class EntityBase<TIdentifier> : IEntity, IEquatable<EntityBase<TIdentifier>>
{
    public abstract TIdentifier Id { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = new();
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
    
    public bool Equals(EntityBase<TIdentifier>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TIdentifier>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EntityBase<TIdentifier>) obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TIdentifier>.Default.GetHashCode(Id);
    }
}