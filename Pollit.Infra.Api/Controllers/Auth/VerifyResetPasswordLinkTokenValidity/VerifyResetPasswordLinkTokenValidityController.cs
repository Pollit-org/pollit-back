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
    public async Task<IActionResult?> VerifyResetPasswordLinkTokenValidityAsync([FromQuery] VerifyResetPasswordLinkTokenValidityHttpQuery requestQueryParams)
    {
        var query = new VerifyResetPasswordLinkTokenValidityQuery(requestQueryParams.UserId, requestQueryParams.ResetPasswordToken);

        var presenter = new VerifyResetPasswordLinkTokenValidityPresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}