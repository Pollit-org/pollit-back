using Pollit.Domain.Users;

namespace Pollit.Application;

public class AuthorizedOperation<TOperation> where TOperation : IOperation
{
    internal AuthorizedOperation(TOperation value, UserId? authorizedFor)
    {
        Value = value;
        AuthorizedFor = authorizedFor;
    }
    
    public TOperation Value { get; }
    public UserId? AuthorizedFor { get; }
}