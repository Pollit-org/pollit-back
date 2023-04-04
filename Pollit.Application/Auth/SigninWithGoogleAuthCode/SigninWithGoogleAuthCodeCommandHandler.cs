using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeCommandHandler : CommandHandlerBase<SigninWithGoogleAuthCodeCommand, ISigninWithGoogleAuthCodePresenter>
{
    private readonly GoogleAuthenticationService _googleAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithGoogleAuthCodeCommandHandler(IUnitOfWork unitOfWork, GoogleAuthenticationService googleAuthenticationService)
    {
        _unitOfWork = unitOfWork;
        _googleAuthenticationService = googleAuthenticationService;
    }
    
    protected override async Task HandleAsync(AuthorizedCommand<SigninWithGoogleAuthCodeCommand> authorizedCommand, ISigninWithGoogleAuthCodePresenter presenter)
    {
        var result = await _googleAuthenticationService.SigninWithGoogleAuthCodeAsync(authorizedCommand.Command.GoogleAuthenticationCode);

        await result.SwitchAsync(
            async signinResult =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success(signinResult);
            },
            googleAuthCodeAuthenticationError => presenter.GoogleAuthCodeAuthenticationFailed()
        );
    }

    protected override Task<bool> IsAuthorized(UserId? userId, SigninWithGoogleAuthCodeCommand command) => Task.FromResult(userId is null);
}