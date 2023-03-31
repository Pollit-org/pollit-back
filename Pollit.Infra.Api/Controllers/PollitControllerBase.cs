using Microsoft.AspNetCore.Mvc;
using Pollit.Application;

namespace Pollit.Infra.Api.Controllers;

public class PollitControllerBase : ControllerBase
{
    
}

public abstract class CommandControllerBase<TCommand, TPresenter, TPresenterImpl, TCommandHandler> : PollitControllerBase 
    where TCommandHandler : CommandHandlerBase<TCommand, TPresenter> where TPresenterImpl: BasePresenter, TPresenter
{
    private readonly TCommandHandler _commandHandler;

    public CommandControllerBase(TCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    protected async Task HandleCommandAsync(TCommand command, TPresenterImpl presenter)
    {
        try
        {
            await _commandHandler.HandleAsync(command, presenter);
        }
        catch (Exception exception)
        {
            if (exception is PollitApplicationException e) 
                presenter.BadRequest(e.ErrorCode);
        }
    }
}