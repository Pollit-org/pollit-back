using Pollit.Application._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IUnitOfWork _unitOfWork;

    public SigninWithCredentialsCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordEncryptor passwordEncryptor, IAccessTokenManager accessTokenManager)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _accessTokenManager = accessTokenManager;
    }

    public async Task HandleAsync(SigninWithCredentialsCommand command, ISigninWithCredentialsPresenter presenter)
    {
        User? user = null;
        if (Email.TryParse(command.UserNameOrEmail, out var email))
            user = await _userRepository.FindByEmailAsync(email);
        else if (UserName.TryParse(command.UserNameOrEmail, out var userName))
            user = await _userRepository.FindByUserNameAsync(userName);

        if (user?.EncryptedPassword is null)
        {
            presenter.LoginFailed();
            return;
        }

        if (!_passwordEncryptor.ValidateClearPasswordAgainstEncrypted(command.Password, user.EncryptedPassword))
        {
            presenter.LoginFailed();
            return;
        }
        
        user.OnLoginWithCredentials();

        var accessToken = _accessTokenManager.Build(user.GetClaims());
        var refreshToken = RefreshToken.Generate();
        
        user.AddRefreshToken(refreshToken);
        
        await _unitOfWork.SaveChangesAsync();
        
        presenter.Success(new SigninResult(accessToken, refreshToken));
    }
}