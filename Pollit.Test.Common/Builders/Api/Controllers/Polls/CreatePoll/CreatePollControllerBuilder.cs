using Pollit.Application.Polls.CreatePoll;
using Pollit.Infra.Api.Controllers.Polls.CreatePoll;

namespace Pollit.Test.Common.Builders.Api.Controllers.Polls.CreatePoll;

public class CreatePollControllerBuilder : ControllerBuilderBase<CreatePollController>
{
    private CreatePollCommandHandler _commandHandler;

    public CreatePollControllerBuilder WithCommandHandler(CreatePollCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public override CreatePollController Build()
    {
        return new CreatePollController(_commandHandler, _authenticatedUserProvider);
    }
}