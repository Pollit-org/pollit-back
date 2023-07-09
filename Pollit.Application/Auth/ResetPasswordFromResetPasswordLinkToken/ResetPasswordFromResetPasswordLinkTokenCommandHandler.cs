using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;

public class ResetPasswordFromResetPasswordLinkTokenCommandHandler : OperationHandlerBase<ResetPasswordFromResetPasswordLinkTokenCommand, IResetPasswordFromResetPasswordLinkTokenPresenter>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CredentialsAuthenticationService _credentialsAuthenticationService;

    public ResetPasswordFromResetPasswordLinkTokenCommandHandler(CredentialsAuthenticationService credentialsAuthenticationService, IUnitOfWork unitOfWork)
    {
        _credentialsAuthenticationService = credentialsAuthenticationService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<ResetPasswordFromResetPasswordLinkTokenCommand> command, IResetPasswordFromResetPasswordLinkTokenPresenter presenter)
    {
        var result = await _credentialsAuthenticationService.ResetPasswordFromResetPasswordLinkToken(command.Value.UserId, command.Value.ResetPasswordToken, command.Value.NewPassword);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success();
            },
            userDoesNotExistError => presenter.UserDoesNotExist(),
            resetPasswordLinkNotFoundOrExpiredError => presenter.ResetPasswordLinkNotFoundOrExpired()
        );
    }
}