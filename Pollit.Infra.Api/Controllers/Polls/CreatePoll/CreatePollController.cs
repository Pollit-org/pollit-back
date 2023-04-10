using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Polls.CreatePoll;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Polls.CreatePoll;

[ApiController]
public class CreatePollController : OperationControllerBase<CreatePollCommand, ICreatePollPresenter, CreatePollPresenter, CreatePollCommandHandler>
{
    public CreatePollController(CreatePollCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserprovider) : base(commandHandler, authenticatedUserprovider)
    {
    }

    [Authorize(Policy = CPolicies.PermanentUserNameAndEmailVerified)]
    [HttpPost("polls", Name = "CreatePoll")]
    public async Task<IActionResult?> CreatePollAsync([FromBody] CreatePollHttpRequestBody requestBody)
    {
        var command = new CreatePollCommand(AuthenticatedUserId, requestBody.Title, requestBody.Tags, requestBody.Options);

        var presenter = new CreatePollPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}