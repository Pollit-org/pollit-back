using Newtonsoft.Json;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

namespace Pollit.Test.Common.Builders.Api.Controllers.SigninWithCredentialsControllerBuilder;

public class SigninWithCredentialsHttpRequestBodyBuilder : IFluentBuilder<SigninWithCredentialsHttpRequestBody>
{
    private SigninWithCredentialsHttpRequestBody _requestBody = new()
    {
        EmailOrUserName = "dwdawd@pollit.me",
        Password = "NotYourBusiness12345++",
    };
    
    public SigninWithCredentialsHttpRequestBodyBuilder WithEmailOrUserName(string emailOrUserName)
    {
        _requestBody.EmailOrUserName = emailOrUserName;
        return this;
    }

    public SigninWithCredentialsHttpRequestBodyBuilder WithPassword(string password) 
    {
        _requestBody.Password = password;
        return this;
    }

    public SigninWithCredentialsHttpRequestBody Build()
    {
        return JsonConvert.DeserializeObject<SigninWithCredentialsHttpRequestBody>(JsonConvert.SerializeObject(_requestBody));
    }
}