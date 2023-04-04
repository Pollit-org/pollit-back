using Microsoft.AspNetCore.Mvc;
using Pollit.Application;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers;

public class PollitControllerBase : ControllerBase
{
    
}

public abstract class CommandControllerBase<TCommand, TPresenter, TPresenterImpl, TCommandHandler> : PollitControllerBase 
    where TCommandHandler : CommandHandlerBase<TCommand, TPresenter>
    where TPresenter : IPresenter
    where TPresenterImpl: BasePresenter, TPresenter
    where TCommand : ICommand
{
    private readonly TCommandHandler _commandHandler;

    public CommandControllerBase(TCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }
    
    public UserId? AuthenticatedUserId
    {
        get
        {
            var value = HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == CClaimTypes.UserId)?.Value;
            return value != null ? new UserId(Guid.Parse(value)) : null;
        }
    }

    protected async Task HandleCommandAsync(TCommand command, TPresenterImpl presenter)
    {
        try
        {
            await _commandHandler.HandleAsync(AuthenticatedUserId, command, presenter);
        }
        catch (Exception exception)
        {
            if (exception is PollitApplicationException e) 
                presenter.BadRequest(e.ErrorCode);
        }
    }
}