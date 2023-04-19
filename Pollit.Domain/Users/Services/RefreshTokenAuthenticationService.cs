using OneOf;
using Pollit.Domain._Ports;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.Errors;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SigninWithRefreshTokenResult : OneOfBase<SigninResult, ExpiredAccessTokenIsInvalidError, UserDoesNotExistError, InvalidRefreshTokenError> {}

public class RefreshTokenAuthenticationService
{
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IUserRepository _userRepository;

    public RefreshTokenAuthenticationService(IAccessTokenManager accessTokenManager, IUserRepository userRepository)
    {
        _accessTokenManager = accessTokenManager;
        _userRepository = userRepository;
    }

    public async Task<SigninWithRefreshTokenResult> SigninWithExpiredAccessTokenAndRefreshTokenAsync(AccessToken expiredAccessToken, RefreshToken refreshToken)
    {
        if (!_accessTokenManager.IsValid(expiredAccessToken, false, out var claims))
            return new ExpiredAccessTokenIsInvalidError();

        if (!claims.TryGetValue(CClaimTypes.UserId, out var userId))
            return new ExpiredAccessTokenIsInvalidError();

        var user = await _userRepository.GetAsync(Guid.Parse(userId));
        if (user is null)
            return new UserDoesNotExistError();

        if (!user.RemoveRefreshToken(refreshToken))
            return new InvalidRefreshTokenError();
        
        var newAccessToken = _accessTokenManager.Build(user.GetClaims());
        var newRefreshToken = RefreshToken.Generate();
        
        user.AddRefreshToken(newRefreshToken);
        
        user.OnSigninWithRefreshToken();
        
        return new SigninResult(newAccessToken, newRefreshToken);
    }
}