namespace Pollit.Infra.FrontApp.UrlBuilder.Config;

public class FrontAppVerifyEmailRouteConfig
{
    public string Path { get; set; }
    public string VerificationTokenParamName { get; set; }
    public string UserIdParamName { get; set; }
}