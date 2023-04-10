namespace Pollit.Application.Auth.SigninWithGoogleAuthCode;

[OperationAuthorizedForAnyone]
public class SigninWithGoogleAuthCodeCommand : IOperation
{
    public SigninWithGoogleAuthCodeCommand(string googleAuthenticationCode)
    {
        GoogleAuthenticationCode = googleAuthenticationCode;
    }
    
    public string GoogleAuthenticationCode { get; }
}