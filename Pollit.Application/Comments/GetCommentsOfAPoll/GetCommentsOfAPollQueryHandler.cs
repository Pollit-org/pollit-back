namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollQueryHandler : OperationHandlerBase<GetCommentsOfAPollQuery, IGetCommentsOfAPollPresenter>
{
    private readonly IGetCommentsOfAPollProjection _getCommentsOfAPollProjection;

    public GetCommentsOfAPollQueryHandler(IGetCommentsOfAPollProjection getCommentsOfAPollProjection)
    {
        _getCommentsOfAPollProjection = getCommentsOfAPollProjection;
    }

    protected override Task HandleAsync(AuthorizedOperation<GetCommentsOfAPollQuery> query, IGetCommentsOfAPollPresenter presenter)
    {
        var result = _getCommentsOfAPollProjection.GetCommentsOfAPoll(query.Value, query.AuthorizedFor);
        
        presenter.Success(result);

        return Task.CompletedTask;
    }
}