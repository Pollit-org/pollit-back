using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public interface IGetCommentsOfAPollPresenter : IPresenter
{
    void Success(PaginationResult<GetCommentsOfAPollQueryResultItem> result);
    
    void PollDoesNotExist(string error = ApplicationError.PollNotFound);
}