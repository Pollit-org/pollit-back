using Newtonsoft.Json;
using Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

namespace Pollit.Test.Common.Builders.Api.Controllers.SignupWithCredentialsControllerBuilder;

public class SignupWithCredentialsHttpRequestBodyBuilder : IFluentBuilder<SignupWithCredentialsHttpRequestBody>
{
    private SignupWithCredentialsHttpRequestBody _requestBody = new()
    {
        Email = "dbhqwfbweuhfbwe@pollit.me",
        UserName = "dbhqwfbweuhfbwe",
        Password = "NotYourBusiness12345++"
    };
    
    public SignupWithCredentialsHttpRequestBodyBuilder WithEmail(string email) 
    {
        _requestBody.Email = email;
        return this;
    }
    
    
    public SignupWithCredentialsHttpRequestBodyBuilder WithUserName(string userName) 
    {
        _requestBody.UserName = userName;
        return this;
    }
    

    public SignupWithCredentialsHttpRequestBodyBuilder WithPassword(string password) 
    {
        _requestBody.Password = password;
        return this;
    }
    
    public SignupWithCredentialsHttpRequestBodyBuilder WithValidCredentials()
    {
        _requestBody.Email = "nice-guy@pollit.me";
        _requestBody.UserName = "Some-Nice-Guy-97";
        _requestBody.Password = "NiceGuyPass@word123";
        return this;
    }
    
    public SignupWithCredentialsHttpRequestBody Build()
    {
        return JsonConvert.DeserializeObject<SignupWithCredentialsHttpRequestBody>(JsonConvert.SerializeObject(_requestBody));
    }
}