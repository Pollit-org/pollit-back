using OneOf;
using OneOf.Types;
using Pollit.Domain._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.ClearPasswords;
using Pollit.Domain.Users.Errors;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SignupWithCredentialsResult : OneOfBase<Success, EmailAlreadyExistsError, UserNameAlreadyExistsError> { }

[GenerateOneOf]
public partial class SigninWithCredentialsResult : OneOfBase<SigninResultDto, UserDoesNotExistError, UserHasNoPasswordError, PasswordMismatchError> { }

public class CredentialsAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IAccessTokenManager _accessTokenManager;
    
    public CredentialsAuthenticationService(IUserRepository userRepository,IPasswordEncryptor passwordEncryptor, IAccessTokenManager accessTokenManager)
    {
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _accessTokenManager = accessTokenManager;
    }

    public async Task<SignupWithCredentialsResult> SignupWithCredentialsAsync(Email email, UserName userName, ClearPassword clearPassword)
    {
        if (await _userRepository.EmailExistsAsync(email))
            return new EmailAlreadyExistsError();

        if (await _userRepository.UserNameExistsAsync(userName))
            return new UserNameAlreadyExistsError();

        var encryptedPassword = _passwordEncryptor.Encrypt(clearPassword);

        var user = User.NewUser(email, userName, encryptedPassword);

        await _userRepository.AddAsync(user);

        return new Success();
    }

    public async Task<SigninWithCredentialsResult> SigninWithCredentialsAsync(string userNameOrEmail, ClearPassword clearPassword)
    {
        User? user = null;
        if (Email.TryParse(userNameOrEmail, out var email))
            user = await _userRepository.FindByEmailAsync(email);
        else if (UserName.TryParse(userNameOrEmail, out var userName))
            user = await _userRepository.FindByUserNameAsync(userName);

        if (user is null)
            return new UserDoesNotExistError();

        if (user.EncryptedPassword is null)
            return new UserHasNoPasswordError();

        if (!_passwordEncryptor.ValidateClearPasswordAgainstEncrypted(clearPassword, user.EncryptedPassword))
            return new PasswordMismatchError();

        user.OnSigninWithCredentials();

        var accessToken = _accessTokenManager.Build(user.GetClaims());
        var refreshToken = RefreshToken.Generate();
        
        user.AddRefreshToken(refreshToken);
        
        return new SigninResultDto(accessToken, refreshToken);
    }
}