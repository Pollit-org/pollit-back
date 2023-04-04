namespace Pollit.Domain.Users;

public class SigninResultDto
{
    public SigninResultDto(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
}