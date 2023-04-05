using Pollit.Domain.Users;

namespace Pollit.Application;

public abstract class OperationAuthorizationPropertyAttributeBase : Attribute
{
    public abstract bool SatisfiesAuthorization<TOperation>(UserId? userId, TOperation operation, object? queryField) where TOperation : IOperation;
}