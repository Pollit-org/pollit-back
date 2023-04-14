using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

public interface IPollFeedProjection
{
    PaginationResult<GetPollFeedQueryResultItem> GetPolLFeed(GetPollFeedQuery query);
}