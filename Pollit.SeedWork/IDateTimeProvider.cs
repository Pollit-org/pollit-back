namespace Pollit.SeedWork;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}