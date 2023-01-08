using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommand
{
    public SignupWithCredentialsCommand(Email email, ClearPassword password)
    {
        Email = email;
        Password = password;
    }

    public Email Email { get; }
    
    public ClearPassword Password { get; }
}