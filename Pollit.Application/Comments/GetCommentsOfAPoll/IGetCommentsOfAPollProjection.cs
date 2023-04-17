using Pollit.Domain.Users;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public interface IGetCommentsOfAPollProjection
{
    PaginationResult<GetCommentsOfAPollQueryResultItem> GetCommentsOfAPoll(GetCommentsOfAPollQuery query, UserId? requestingUserId);
}