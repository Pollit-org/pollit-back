using OneOf;
using Pollit.Domain._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.Errors;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SigninWithGoogleAuthCodeResult : OneOfBase<SigninResultDto, GoogleAuthCodeAuthenticationError> { }

public class GoogleAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IGoogleAuthenticator _googleAuthenticator;
    
    public GoogleAuthenticationService(IUserRepository userRepository, IAccessTokenManager accessTokenManager, IGoogleAuthenticator googleAuthenticator)
    {
        _userRepository = userRepository;
        _accessTokenManager = accessTokenManager;
        _googleAuthenticator = googleAuthenticator;
    }
    
    public async Task<SigninWithGoogleAuthCodeResult> SigninWithGoogleAuthCodeAsync(string googleAuthCode)
    {
        GoogleProfileDto googleProfile;
        try
        {
            googleProfile = await _googleAuthenticator.AuthenticateAsync(googleAuthCode);
        }
        catch (Exception e)
        {
            return new GoogleAuthCodeAuthenticationError();
        }

        var email = new Email(googleProfile.Email);
        
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
        {
            user = User.NewUser(email);
            await _userRepository.AddAsync(user);
        }

        user.OnLoginWithGoogle(googleProfile);
        
        var accessToken = _accessTokenManager.Build(user.GetClaims());
        var refreshToken = RefreshToken.Generate();
        
        user.AddRefreshToken(refreshToken);
        
        return new SigninResultDto(accessToken, refreshToken);
    }
}