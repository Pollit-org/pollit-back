using Microsoft.AspNetCore.Mvc;
using Pollit.Application;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers;

public class PollitControllerBase : ControllerBase
{
    
}

public abstract class OperationControllerBase<TCommand, TPresenter, TPresenterImpl, TOperationHandler> : PollitControllerBase 
    where TOperationHandler : OperationHandlerBase<TCommand, TPresenter>
    where TPresenter : IPresenter
    where TPresenterImpl: BasePresenter, TPresenter
    where TCommand : IOperation
{
    private readonly TOperationHandler _opHandler;

    public OperationControllerBase(TOperationHandler opHandler)
    {
        _opHandler = opHandler;
    }
    
    public UserId? AuthenticatedUserId
    {
        get
        {
            var value = HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == CClaimTypes.UserId)?.Value;
            return value != null ? new UserId(Guid.Parse(value)) : null;
        }
    }

    protected async Task HandleOperationAsync(TCommand command, TPresenterImpl presenter)
    {
        try
        {
            await _opHandler.HandleAsync(AuthenticatedUserId, command, presenter);
        }
        catch (Exception exception)
        {
            if (exception is PollitApplicationException e) 
                presenter.BadRequest(e.ErrorCode);
        }
    }
}