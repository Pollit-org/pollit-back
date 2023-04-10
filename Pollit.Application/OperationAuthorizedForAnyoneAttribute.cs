using Pollit.Domain.Users;

namespace Pollit.Application;

[AttributeUsage(AttributeTargets.Class)]
public class OperationAuthorizedForAnyoneAttribute : OperationAuthorizationClassAttributeBase
{
    public override bool SatisfiesAuthorization<TQuery>(UserId? userId, TQuery query)
    {
        return true;
    }
}