namespace Pollit.SeedWork.Eventing;

public abstract class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        OccurredOn = DateTime.Now;
    }

    public DateTime OccurredOn { get; }
}