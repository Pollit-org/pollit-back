using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SigninWithRefreshToken;

public class SigninWithRefreshTokenCommandHandler : OperationHandlerBase<SigninWithRefreshTokenCommand, ISigninWithRefreshTokenPresenter>
{
    private readonly RefreshTokenAuthenticationService _refreshTokenAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithRefreshTokenCommandHandler(RefreshTokenAuthenticationService refreshTokenAuthenticationService, IUnitOfWork unitOfWork)
    {
        _refreshTokenAuthenticationService = refreshTokenAuthenticationService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<SigninWithRefreshTokenCommand> command, ISigninWithRefreshTokenPresenter presenter)
    {
        var result = await _refreshTokenAuthenticationService.SigninWithExpiredAccessTokenAndRefreshTokenAsync(command.Value.ExpiredAccessToken, command.Value.RefreshToken);

        await result.SwitchAsync(
            async signinResult =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success(signinResult);
            },
            expiredAccessTokenIsInvalidError => presenter.ExpiredAccessTokenIsInvalid(),
            userDoesNotExistError => presenter.UserDoesNotExist(),
            invalidRefreshTokenError => presenter.RefreshTokenIsInvalid()
        );
    }
}