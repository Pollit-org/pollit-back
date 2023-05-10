using System.Reflection;

namespace Pollit.SeedWork.Eventing;

public class DomainEventBus
{
    private readonly ICollection<Delegate> _handlers = new List<Delegate>();

    public void Emit(IDomainEvent @event)
    {
        foreach (var action in _handlers.Where(a => a.GetMethodInfo().GetParameters().FirstOrDefault()?.ParameterType == @event.GetType()))
            action.DynamicInvoke(@event);
    }

    public void RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
    {
        _handlers.Add(handler);
    }
}