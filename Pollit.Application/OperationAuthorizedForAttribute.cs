using Pollit.Domain.Users;

namespace Pollit.Application;

[AttributeUsage(AttributeTargets.Property)]
public class OperationAuthorizedForAttribute : OperationAuthorizationPropertyAttributeBase
{
    public override bool SatisfiesAuthorization<TQuery>(UserId? userId, TQuery query, object? queryField)
    {
        return userId is not null && queryField is Guid guid && guid == userId;
    }
}