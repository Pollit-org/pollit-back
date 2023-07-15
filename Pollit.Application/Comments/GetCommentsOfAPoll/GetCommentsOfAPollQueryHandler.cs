using Pollit.Domain.Polls._Ports;

namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollQueryHandler : OperationHandlerBase<GetCommentsOfAPollQuery, IGetCommentsOfAPollPresenter>
{
    private readonly IPollRepository _pollRepository;
    private readonly IGetCommentsOfAPollProjection _getCommentsOfAPollProjection;

    public GetCommentsOfAPollQueryHandler(IGetCommentsOfAPollProjection getCommentsOfAPollProjection, IPollRepository pollRepository)
    {
        _getCommentsOfAPollProjection = getCommentsOfAPollProjection;
        _pollRepository = pollRepository;
    }

    protected override async Task HandleAsync(AuthorizedOperation<GetCommentsOfAPollQuery> query, IGetCommentsOfAPollPresenter presenter)
    {
        if (! await _pollRepository.ExistsAsync(query.Value.PollId))
        {
            presenter.PollDoesNotExist();
            return;
        }
        
        var result = _getCommentsOfAPollProjection.GetCommentsOfAPoll(query.Value, query.AuthorizedFor);
        
        presenter.Success(result);
    }
}