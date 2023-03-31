namespace Pollit.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommand
{
    public SignupWithCredentialsCommand(string email, string userName, string password)
    {
        Email = email;
        UserName = userName;
        Password = password;
    }

    public string Email { get; }
    
    public string UserName { get; }

    public string Password { get; }
}