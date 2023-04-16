namespace Pollit.Application.Polls.GetPollFeed;

public class GetPollFeedQueryHandler : OperationHandlerBase<GetPollFeedQuery, IGetPollFeedPresenter>
{
    private readonly IPollFeedProjection _pollFeedProjection;

    public GetPollFeedQueryHandler(IPollFeedProjection pollFeedProjection)
    {
        _pollFeedProjection = pollFeedProjection;
    }

    protected override Task HandleAsync(AuthorizedOperation<GetPollFeedQuery> query, IGetPollFeedPresenter presenter)
    {
        var result = _pollFeedProjection.GetPollFeed(query.Value, query.AuthorizedFor);
        
        presenter.Success(result);

        return Task.CompletedTask;
    }
}