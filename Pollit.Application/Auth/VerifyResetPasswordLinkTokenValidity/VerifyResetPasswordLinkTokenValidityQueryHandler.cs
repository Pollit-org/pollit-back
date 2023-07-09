using Pollit.Domain.Users._Ports;

namespace Pollit.Application.Auth.VerifyResetPasswordLinkTokenValidity;

public class VerifyResetPasswordLinkTokenValidityQueryHandler : OperationHandlerBase<VerifyResetPasswordLinkTokenValidityQuery, IVerifyResetPasswordLinkTokenValidityPresenter>
{
    private readonly IUserRepository _userRepository;

    public VerifyResetPasswordLinkTokenValidityQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleAsync(AuthorizedOperation<VerifyResetPasswordLinkTokenValidityQuery> query, IVerifyResetPasswordLinkTokenValidityPresenter presenter)
    {
        var user = await _userRepository.FindByIdAsync(query.Value.UserId);
        if (user is null)
        {
            presenter.Success(false);
            return;
        }

        var result = user.HasAnyResetPasswordLinkConsumableByToken(query.Value.ResetPasswordToken);
        presenter.Success(result);
    }
}