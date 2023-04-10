using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommandHandler : OperationHandlerBase<SigninWithCredentialsCommand, ISigninWithCredentialsPresenter>
{
    private readonly CredentialsAuthenticationService _credentialsAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithCredentialsCommandHandler(IUnitOfWork unitOfWork, CredentialsAuthenticationService credentialsAuthenticationService)
    {
        _unitOfWork = unitOfWork;
        _credentialsAuthenticationService = credentialsAuthenticationService;
    }

    protected override async Task HandleAsync(AuthorizedOperation<SigninWithCredentialsCommand> authorizedCommand, ISigninWithCredentialsPresenter presenter)
    {
        var result = await _credentialsAuthenticationService.SigninWithCredentialsAsync(authorizedCommand.Value.UserNameOrEmail, authorizedCommand.Value.Password);

        await result.SwitchAsync(
            async signinResult =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success(signinResult);
            },
            userDoesNotExistError => presenter.LoginFailed(),
            userHasNoPasswordError => presenter.LoginFailed(),
            passwordMismatchError => presenter.LoginFailed()
        );
    }
}