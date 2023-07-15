namespace Pollit.Application.Polls.GetPollResults;

public class GetPollResultsQueryHandler : OperationHandlerBase<GetPollResultsQuery, IGetPollResultsPresenter>
{
    private readonly IPollResultsProjection _pollResultsProjection;

    public GetPollResultsQueryHandler(IPollResultsProjection pollResultsProjection)
    {
        _pollResultsProjection = pollResultsProjection;
    }

    protected override async Task HandleAsync(AuthorizedOperation<GetPollResultsQuery> query, IGetPollResultsPresenter presenter)
    {
        var result = await _pollResultsProjection.GetPollResultsAsync(query.Value);
        
        presenter.Success(result);
    }
}