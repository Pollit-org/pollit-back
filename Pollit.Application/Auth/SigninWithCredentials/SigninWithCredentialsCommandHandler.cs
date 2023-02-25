using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommandHandler : CommandHandlerBase<SigninWithCredentialsCommand, ISigninWithCredentialsPresenter>
{
    private readonly CredentialsAuthenticationService _credentialsAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithCredentialsCommandHandler(IUnitOfWork unitOfWork, CredentialsAuthenticationService credentialsAuthenticationService)
    {
        _unitOfWork = unitOfWork;
        _credentialsAuthenticationService = credentialsAuthenticationService;
    }

    protected override async Task HandleInternalAsync(SigninWithCredentialsCommand command, ISigninWithCredentialsPresenter presenter)
    {
        var result = await _credentialsAuthenticationService.SigninWithCredentialsAsync(command.UserNameOrEmail, command.Password);

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