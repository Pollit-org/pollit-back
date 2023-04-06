using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.GetUserPrivateProfile;

namespace Pollit.Infra.Api.Controllers.Users.GetUserPrivateProfile;

[ApiController]
public class GetUserPrivateProfileController : OperationControllerBase<GetUserPrivateProfileQuery, IGetUserPrivateProfilePresenter, GetUserPrivateProfilePresenter, GetUserPrivateProfileQueryHandler>
{
    public GetUserPrivateProfileController(GetUserPrivateProfileQueryHandler queryHandler) : base(queryHandler)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpGet("users/{userId:guid}/profile/private", Name = "GetUserPrivateProfile")]
    public async Task<IActionResult?> GetUserPrivateProfileAsync(Guid userId)
    {
        var query = new GetUserPrivateProfileQuery(userId);

        var presenter = new GetUserPrivateProfilePresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}