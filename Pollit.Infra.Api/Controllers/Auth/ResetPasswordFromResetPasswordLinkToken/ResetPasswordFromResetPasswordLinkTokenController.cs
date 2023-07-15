using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.ResetPasswordFromResetPasswordLinkToken;

[ApiController]
public class ResetPasswordFromResetPasswordLinkTokenController : OperationControllerBase<ResetPasswordFromResetPasswordLinkTokenCommand, IResetPasswordFromResetPasswordLinkTokenPresenter, ResetPasswordFromResetPasswordLinkTokenPresenter, ResetPasswordFromResetPasswordLinkTokenCommandHandler>
{
    public ResetPasswordFromResetPasswordLinkTokenController(ResetPasswordFromResetPasswordLinkTokenCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/resetPassword/fromResetPasswordToken", Name = "ResetPasswordFromResetPasswordLinkToken")]
    public async Task<IActionResult?> ResetPasswordFromResetPasswordLinkTokenAsync([FromBody] ResetPasswordFromResetPasswordLinkTokenHttpRequestBody requestBody)
    {
        var command = new ResetPasswordFromResetPasswordLinkTokenCommand(requestBody.UserId, requestBody.ResetPasswordToken, requestBody.NewPassword);

        var presenter = new ResetPasswordFromResetPasswordLinkTokenPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}