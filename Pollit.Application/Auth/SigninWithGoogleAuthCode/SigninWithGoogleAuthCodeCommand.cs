namespace Pollit.Application.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeCommand
{
    public SigninWithGoogleAuthCodeCommand(string googleAuthenticationCode)
    {
        GoogleAuthenticationCode = googleAuthenticationCode;
    }
    
    public string GoogleAuthenticationCode { get; }
}