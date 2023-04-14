using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

public interface IGetPollFeedPresenter : IPresenter
{
    void Success(PaginationResult<GetPollFeedQueryResultItem> result);
}