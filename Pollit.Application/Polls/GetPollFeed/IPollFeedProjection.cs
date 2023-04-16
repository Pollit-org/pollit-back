using Pollit.Domain.Users;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

public interface IPollFeedProjection
{
    PaginationResult<GetPollFeedQueryResultItem> GetPollFeed(GetPollFeedQuery query, UserId? requestingUserId);
}