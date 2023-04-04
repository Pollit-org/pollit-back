using Pollit.SeedWork;

namespace Pollit.Application;

public abstract class QueryHandlerBase
{
}

public abstract class QueryHandlerBase<TQuery, TPresenter> : QueryHandlerBase
{
    public async Task HandleAsync(TQuery query, TPresenter presenter)
    {
        try
        {
            await HandleInternalAsync(query, presenter);
        }
        catch (PollitDomainException e)
        {
            throw PollitApplicationException.FromDomainException(e);
        }
    }

    protected abstract Task HandleInternalAsync(TQuery query, TPresenter presenter);
}