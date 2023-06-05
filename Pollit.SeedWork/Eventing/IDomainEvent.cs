
namespace Pollit.SeedWork.Eventing;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}