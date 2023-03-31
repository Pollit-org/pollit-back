using Pollit.Test.Common.Builders.Api.Controllers.SignupWithCredentialsControllerBuilder;
using Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;
using Pollit.Test.InMemoryDb;

namespace Pollit.Infra.Api.Test.x_Scenario;

public class ScenarioWhereIJustSignedUpWithCredentials
{
    private readonly InMemoryDatabase _database;
    private string _email = "bg-du-69@pollit.me";
    private string _userName = "bg-du-69";
    private string _password = "cMoaLeGroBg+++";
    private SignupWithCredentialsHttpRequestBodyBuilder _signupWithCredentialsHttpRequestBodyBuilder = new();

    public ScenarioWhereIJustSignedUpWithCredentials(InMemoryDatabase database)
    {
        _database = database;
    }

    public ScenarioWhereIJustSignedUpWithCredentials WithEmail(string email)
    {
        _signupWithCredentialsHttpRequestBodyBuilder.WithEmail(email);
        return this;
    }
    
    public ScenarioWhereIJustSignedUpWithCredentials WithUserName(string userName)
    {
        _signupWithCredentialsHttpRequestBodyBuilder.WithUserName(userName);
        return this;
    }
    
    public ScenarioWhereIJustSignedUpWithCredentials WithPassword(string password)
    {
        _signupWithCredentialsHttpRequestBodyBuilder.WithPassword(password);
        return this;
    }

    public async Task SetupAsync()
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder(_database).Build();
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
        var requestBody = _signupWithCredentialsHttpRequestBodyBuilder.Build();
        await controller.SignupAsync(requestBody);
    }
}