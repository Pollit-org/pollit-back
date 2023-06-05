namespace Pollit.SeedWork.Eventing;

public interface IDomainEventConsumer
{
}

public interface IDomainEventConsumer<in TEvent> : IDomainEventConsumer where TEvent : IDomainEvent
{
    Task ConsumeAsync(TEvent @event);
}