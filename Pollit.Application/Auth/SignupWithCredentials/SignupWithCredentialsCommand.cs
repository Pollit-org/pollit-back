using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommand
{
    public SignupWithCredentialsCommand(Email email, UserName userName, ClearPassword password)
    {
        Email = email;
        UserName = userName;
        Password = password;
    }

    public Email Email { get; }
    
    public UserName UserName { get; }

    public ClearPassword Password { get; }
}