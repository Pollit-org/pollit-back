using Pollit.SeedWork;

namespace Pollit.Application;

public abstract class CommandHandlerBase<TCommand, TPresenter>
{
    public async Task HandleAsync(TCommand command, TPresenter presenter)
    {
        try
        {
            await HandleInternalAsync(command, presenter);
        }
        catch (PollitDomainException e)
        {
            throw PollitApplicationException.FromDomainException(e);
        }
    }

    protected abstract Task HandleInternalAsync(TCommand command, TPresenter presenter);
}