using OneOf;
using OneOf.Types;
using Pollit.Domain._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.ClearPasswords;
using Pollit.Domain.Users.Errors;
using Pollit.Domain.Users.UserNames;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SignupWithCredentialsResult : OneOfBase<Success, EmailAlreadyExistsError, UserNameAlreadyExistsError> { }

[GenerateOneOf]
public partial class SigninWithCredentialsResult : OneOfBase<SigninResult, UserDoesNotExistError, UserHasNoPasswordError, PasswordMismatchError> { }

[GenerateOneOf]
public partial class RequestResetPasswordLinkResult : OneOfBase<Success, UserDoesNotExistError> { }

public class CredentialsAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public CredentialsAuthenticationService(IUserRepository userRepository,IPasswordEncryptor passwordEncryptor, IAccessTokenManager accessTokenManager, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _accessTokenManager = accessTokenManager;
        _dateTimeProvider = dateTimeProvider;
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
        
        return new SigninResult(accessToken, refreshToken);
    }
    
    public async Task<RequestResetPasswordLinkResult> RequestResetPasswordLink(Email email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            return new UserDoesNotExistError();  
        
        return RequestResetPasswordLink(user);
    }

    public async Task<RequestResetPasswordLinkResult> RequestResetPasswordLink(UserId userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
            return new UserDoesNotExistError();

        return RequestResetPasswordLink(user);
    }
    
    private RequestResetPasswordLinkResult RequestResetPasswordLink(User user)
    {
        user.RequestResetPasswordLink(_dateTimeProvider.UtcNow);

        return new Success();
    }
}