using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

public class SigninResultDto
{
    public string AccessToken { get; }
    public string RefreshToken { get; }
    
    public SigninResultDto(Domain.Users.SigninResult signinResult)
    {
        AccessToken = signinResult.AccessToken;
        RefreshToken = signinResult.RefreshToken;
    }
}