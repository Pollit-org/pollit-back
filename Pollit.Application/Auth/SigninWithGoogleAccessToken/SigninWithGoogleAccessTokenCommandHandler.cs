using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SigninWithGoogleAccessToken;

public class SigninWithGoogleAccessTokenCommandHandler : OperationHandlerBase<SigninWithGoogleAccessTokenCommand, ISigninWithGoogleAccessTokenPresenter>
{
    private readonly GoogleAuthenticationService _googleAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithGoogleAccessTokenCommandHandler(GoogleAuthenticationService googleAuthenticationService, IUnitOfWork unitOfWork)
    {
        _googleAuthenticationService = googleAuthenticationService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<SigninWithGoogleAccessTokenCommand> command, ISigninWithGoogleAccessTokenPresenter presenter)
    {
        var result = await _googleAuthenticationService.SigninWithGoogleAccessTokenAsync(command.Value.GoogleAccessToken);

        await result.SwitchAsync(
            async signinResult =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success(signinResult);
            },
            googleAuthCodeAuthenticationError => presenter.GoogleAccessTokenAuthenticationFailed()
        );
    }
}