using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.RequestResetPasswordLink;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.RequestResetPasswordLink;

[ApiController]
public class RequestResetPasswordLinkController : OperationControllerBase<RequestResetPasswordLinkCommand, IRequestResetPasswordLinkPresenter, RequestResetPasswordLinkPresenter, RequestResetPasswordLinkCommandHandler>
{
    public RequestResetPasswordLinkController(RequestResetPasswordLinkCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/requestResetPasswordLink", Name = "RequestResetPasswordLink")]
    public async Task<IActionResult?> RequestResetPasswordLinkAsync([FromBody] RequestResetPasswordLinkHttpRequestBody requestBody)
    {
        var command = new RequestResetPasswordLinkCommand(requestBody.Email);

        var presenter = new RequestResetPasswordLinkPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}