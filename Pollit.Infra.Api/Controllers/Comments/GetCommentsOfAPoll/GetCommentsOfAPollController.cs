using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.Infra.Api.AuthenticatedUserProviders;
using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.Api.Controllers.Comments.GetCommentsOfAPoll;

[ApiController]
public class GetCommentsOfAPollController : OperationControllerBase<GetCommentsOfAPollQuery, IGetCommentsOfAPollPresenter, GetCommentsOfAPollPresenter, GetCommentsOfAPollQueryHandler>
{
    public GetCommentsOfAPollController(GetCommentsOfAPollQueryHandler queryHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(queryHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpGet("polls/{pollId:guid}/comments", Name = "GetCommentsOfAPoll")]
    public async Task<IActionResult?> GetCommentsOfAPollAsync([FromRoute] Guid pollId, [FromQuery] GetCommentsOfAPollHttpRequestQueryParams requestQueryParams)
    {
        var query = new GetCommentsOfAPollQuery(pollId, EGetCommentsOfAPollOrderBy.CreatedAt, EQueryOrder.Descending, new PaginationOptions(0, 50), requestQueryParams.RootCommentId, requestQueryParams.MaxRecursiveDepth ?? 3);

        var presenter = new GetCommentsOfAPollPresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}