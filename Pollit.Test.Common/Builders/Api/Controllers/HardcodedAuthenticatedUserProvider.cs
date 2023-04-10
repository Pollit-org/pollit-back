using Pollit.Domain.Users;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Test.Common.Builders.Api.Controllers;

public class HardcodedAuthenticatedUserProvider : IAuthenticatedUserProvider
{
    private readonly Guid? _userId;

    public HardcodedAuthenticatedUserProvider(Guid? userId = null)
    {
        _userId = userId;
    }
    
    public UserId? GetAuthenticatedUserId()
    {
        return _userId;
    }
}