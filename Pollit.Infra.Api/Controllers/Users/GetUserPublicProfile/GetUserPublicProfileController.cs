using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.GetUserPublicProfile;

namespace Pollit.Infra.Api.Controllers.Users.GetUserPublicProfile;

[ApiController]
public class GetUserPublicProfileController : OperationControllerBase<GetUserPublicProfileQuery, IGetUserPublicProfilePresenter, GetUserPublicProfilePresenter, GetUserPublicProfileQueryHandler>
{
    public GetUserPublicProfileController(GetUserPublicProfileQueryHandler queryHandler) : base(queryHandler)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpGet("users/{userId:guid}/profile/public", Name = "GetUserPublicProfile")]
    public async Task<IActionResult?> GetUserPublicProfileAsync(Guid userId)
    {
        var query = new GetUserPublicProfileQuery(userId);

        var presenter = new GetUserPublicProfilePresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}