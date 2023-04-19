namespace Pollit.Infra.Api.Controllers.Auth.SigninWithRefreshToken;

public class SigninWithRefreshTokenHttpRequestBody
{
    public string ExpiredAccessToken { get; set; }
    public string RefreshToken { get; set; }
}