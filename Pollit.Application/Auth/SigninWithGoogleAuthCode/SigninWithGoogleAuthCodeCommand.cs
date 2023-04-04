namespace Pollit.Application.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeCommand : ICommand
{
    public SigninWithGoogleAuthCodeCommand(string googleAuthenticationCode)
    {
        GoogleAuthenticationCode = googleAuthenticationCode;
    }
    
    public string GoogleAuthenticationCode { get; }
}