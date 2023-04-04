using Microsoft.AspNetCore.Mvc;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api;

public static class ControllerExtensions
{
    public static UserId GetAuthenticatedUserId(this ControllerBase controller)
    {
        var userIdClaim = controller.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CClaimTypes.UserId);
        return  new UserId(Guid.Parse(userIdClaim?.Value ?? throw new InvalidOperationException("UserId claim not found. Make sure the request has the [Authorize] attribute")));
    }
}