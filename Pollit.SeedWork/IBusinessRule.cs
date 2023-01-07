namespace Pollit.SeedWork;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}