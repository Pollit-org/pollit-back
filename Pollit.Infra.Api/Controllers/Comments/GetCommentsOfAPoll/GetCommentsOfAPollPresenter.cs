using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.Api.Controllers.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollPresenter : BasePresenter, IGetCommentsOfAPollPresenter
{
    public void Success(PaginationResult<GetCommentsOfAPollQueryResultItem> result) => Ok(result);

    public void PollDoesNotExist(string error) => NotFound(error);
}