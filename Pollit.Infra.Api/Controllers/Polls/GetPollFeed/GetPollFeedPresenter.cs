using Pollit.Application.Polls.GetPollFeed;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.Api.Controllers.Polls.GetPollFeed;

public class GetPollFeedPresenter : BasePresenter, IGetPollFeedPresenter
{
    public void Success(PaginationResult<GetPollFeedQueryResultItem> result) 
        => Ok(result);
}