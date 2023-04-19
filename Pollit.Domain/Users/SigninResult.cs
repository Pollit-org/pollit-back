namespace Pollit.Domain.Users;

public class SigninResult
{
    public SigninResult(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
}