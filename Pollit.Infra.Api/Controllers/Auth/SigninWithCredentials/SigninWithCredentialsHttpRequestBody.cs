namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

public class SigninWithCredentialsHttpRequestBody
{
    public string EmailOrUserName { get; set; }

    public string Password { get; set; }
}