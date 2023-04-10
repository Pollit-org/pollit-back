using Pollit.Domain.Users;
using Pollit.SeedWork;

namespace Pollit.Application;

public abstract class OperationHandlerBase
{
}

public abstract class OperationHandlerBase<TOperation, TPresenter> : OperationHandlerBase
    where TOperation : IOperation
    where TPresenter : IPresenter
{
    public async Task HandleAsync(UserId? userId, TOperation query, TPresenter presenter)
    {
        try
        {
            if (!await IsAuthorized(userId, query))
            {
                presenter.Forbidden(ApplicationError.ForbiddenAccess);

                return;
            }

            await HandleAsync(new AuthorizedOperation<TOperation>(query, userId), presenter);
        }
        catch (PollitDomainException e)
        {
            throw PollitApplicationException.FromDomainException(e);
        }
    }

    protected abstract Task HandleAsync(AuthorizedOperation<TOperation> authorizedOperation, TPresenter presenter);

    protected virtual Task<bool> IsAuthorized(UserId? userId, TOperation query)
    {
        var hasAtLeastOneAuthorizationAttribute = false;
        
        var classAttrs = typeof(TOperation).GetCustomAttributes(true);
        foreach (var classAttr in classAttrs)
        {
            if (classAttr is not OperationAuthorizationClassAttributeBase queryAuthorizationClassAttr)
                continue;
            
            if (!queryAuthorizationClassAttr.SatisfiesAuthorization(userId, query))
                return Task.FromResult(false);

            hasAtLeastOneAuthorizationAttribute = true;
        }
        
        var queryProps = typeof(TOperation).GetProperties();
        foreach (var prop in queryProps)
        {
            var propAttrs = prop.GetCustomAttributes(true);
            foreach (var propAttr in propAttrs)
            {
                if (propAttr is not OperationAuthorizationPropertyAttributeBase queryAuthorizationPropAttr) 
                    continue;
                
                hasAtLeastOneAuthorizationAttribute = true;
                
                if (!queryAuthorizationPropAttr.SatisfiesAuthorization(userId, query, prop.GetValue(query)))
                    return Task.FromResult(false);
            }
        }

        if (hasAtLeastOneAuthorizationAttribute)
            return Task.FromResult(true);
        
        var currentMethodIsOverriden = GetType() != typeof(OperationHandlerBase<TOperation, TPresenter>);
        if (!currentMethodIsOverriden)
        {
            throw new Exception("No OperationAttribute was found on the Command or Query. Consider adding at least one, or overriding the IsAuthorized method from your OperationHandler");
        }

        return Task.FromResult(true);
    }
}