using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.VerifyResetPasswordLinkTokenValidity;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.VerifyResetPasswordLinkTokenValidity;

[ApiController]
public class VerifyResetPasswordLinkTokenValidityController : OperationControllerBase<VerifyResetPasswordLinkTokenValidityQuery, IVerifyResetPasswordLinkTokenValidityPresenter, VerifyResetPasswordLinkTokenValidityPresenter, VerifyResetPasswordLinkTokenValidityQueryHandler>
{
    public VerifyResetPasswordLinkTokenValidityController(VerifyResetPasswordLinkTokenValidityQueryHandler queryHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(queryHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpGet("auth/resetPassword/fromResetPasswordToken/verifyValidity", Name = "VerifyResetPasswordLinkTokenValidity")]
    public async Task<IActionResult?> VerifyResetPasswordLinkTokenValidityAsync([FromBody] VerifyResetPasswordLinkTokenValidityHttpRequestBody requestBody)
    {
        var query = new VerifyResetPasswordLinkTokenValidityQuery(requestBody.UserId, requestBody.ResetPasswordToken);

        var presenter = new VerifyResetPasswordLinkTokenValidityPresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}