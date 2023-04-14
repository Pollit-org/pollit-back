using Pollit.Domain.Users;

namespace Pollit.Infra.Api.AuthenticatedUserProviders;

public class HttpContextAuthenticatedUserProvider : IAuthenticatedUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextAuthenticatedUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public UserId? GetAuthenticatedUserId()
    {
        var value = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == CClaimTypes.UserId)?.Value;
        return value != null ? new UserId(Guid.Parse(value)) : null;
    }
}