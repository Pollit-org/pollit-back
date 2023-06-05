using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.VerifyUserEmail;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Users.VerifyUserEmail;

[ApiController]
public class VerifyUserEmailController : OperationControllerBase<VerifyUserEmailCommand, IVerifyUserEmailPresenter, VerifyUserEmailPresenter, VerifyUserEmailCommandHandler>
{
    public VerifyUserEmailController(VerifyUserEmailCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("users/{userId:guid}/verify-email", Name = "VerifyUserEmail")]
    public async Task<IActionResult?> VerifyUserEmailAsync([FromRoute] Guid userId, [FromBody] VerifyUserEmailHttpRequestQueryParams requestQueryParams)
    {
        var command = new VerifyUserEmailCommand(requestQueryParams.EmailVerificationToken, userId);

        var presenter = new VerifyUserEmailPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}