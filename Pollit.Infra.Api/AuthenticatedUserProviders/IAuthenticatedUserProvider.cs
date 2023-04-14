using Pollit.Domain.Users;

namespace Pollit.Infra.Api.AuthenticatedUserProviders;

public interface IAuthenticatedUserProvider
{
    UserId? GetAuthenticatedUserId();
}