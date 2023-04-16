using Microsoft.AspNetCore.Mvc;
using Pollit.Application;
using Pollit.Domain.Users;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers;

public class PollitControllerBase : ControllerBase
{
    private readonly IAuthenticatedUserProvider _authenticatedUserProvider;

    public PollitControllerBase(IAuthenticatedUserProvider authenticatedUserProvider)
    {
        _authenticatedUserProvider = authenticatedUserProvider;
    }

    public UserId? AuthenticatedUserId => _authenticatedUserProvider.GetAuthenticatedUserId();
}

public abstract class OperationControllerBase<TCommand, TPresenter, TPresenterImpl, TOperationHandler> : PollitControllerBase 
    where TOperationHandler : OperationHandlerBase<TCommand, TPresenter>
    where TPresenter : IPresenter
    where TPresenterImpl: BasePresenter, TPresenter
    where TCommand : IOperation
{
    private readonly TOperationHandler _opHandler;

    public OperationControllerBase(TOperationHandler opHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(authenticatedUserProvider)
    {
        _opHandler = opHandler;
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
            else
                throw;
        }
    }
}