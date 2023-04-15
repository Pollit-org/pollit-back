using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Polls.CastVoteToPoll;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Polls.CastVoteToPoll;

[ApiController]
public class CastVoteToPollController : OperationControllerBase<CastVoteToPollCommand, ICastVoteToPollPresenter, CastVoteToPollPresenter, CastVoteToPollCommandHandler>
{
    public CastVoteToPollController(CastVoteToPollCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPost("polls/{pollId:guid}/options/{pollOptionId:guid}/votes", Name = "CastVoteToPoll")]
    public async Task<IActionResult?> CastVoteToPollAsync([FromRoute] Guid pollId, [FromRoute] Guid pollOptionId)
    {
        var command = new CastVoteToPollCommand(AuthenticatedUserId, pollId, pollOptionId);

        var presenter = new CastVoteToPollPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}

public class CastVoteToPollPresenter : BasePresenter, ICastVoteToPollPresenter
{
    public void Success() => OkNoContent();

    public void VoterNotFound(string error) => NotFound(error);

    public void PollNotFound(string error) => NotFound(error);

    public void OptionNotFound(string error) => NotFound(error);

    public void UserHasAlreadyVoted(string error) => Conflict(error);
}