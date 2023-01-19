using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommand
{
    public SigninWithCredentialsCommand(string userNameOrEmail, ClearPassword password)
    {
        UserNameOrEmail = userNameOrEmail;
        Password = password;
    }

    public string UserNameOrEmail { get; }
    public ClearPassword Password { get; }
}