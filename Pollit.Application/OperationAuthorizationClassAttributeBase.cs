using Pollit.Domain.Users;

namespace Pollit.Application;

public abstract class OperationAuthorizationClassAttributeBase : Attribute
{
    public abstract bool SatisfiesAuthorization<TOperation>(UserId? userId, TOperation query) where TOperation : IOperation;
}