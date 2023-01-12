using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Application.Auth.SigninWithGoogle;

public class SigninWithGoogleCommand
{
    public SigninWithGoogleCommand(string googleAuthenticationCode)
    {
        GoogleAuthenticationCode = googleAuthenticationCode;
    }
    
    public string GoogleAuthenticationCode { get; }
}