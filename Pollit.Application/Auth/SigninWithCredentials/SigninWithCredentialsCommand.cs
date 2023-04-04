using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommand : ICommand
{
    public SigninWithCredentialsCommand(string userNameOrEmail, string password)
    {
        UserNameOrEmail = userNameOrEmail;
        Password = password;
    }

    public string UserNameOrEmail { get; }
    public string Password { get; }
}