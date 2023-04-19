namespace Pollit.Application.Auth.SigninWithRefreshToken;

public class SigninWithRefreshTokenCommand : IOperation
{
    public SigninWithRefreshTokenCommand(string expiredAccessToken, string refreshToken)
    {
        ExpiredAccessToken = expiredAccessToken;
        RefreshToken = refreshToken;
    }

    public string ExpiredAccessToken { get; }
    public string RefreshToken { get; }
}