namespace Pollit.Application.Auth.SigninWithGoogleAccessToken;

public class SigninWithGoogleAccessTokenCommand : IOperation
{
    public SigninWithGoogleAccessTokenCommand(string googleAccessToken)
    {
        GoogleAccessToken = googleAccessToken;
    }

    public string GoogleAccessToken { get; }
}