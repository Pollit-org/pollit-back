using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Polls.GetPollFeed;
using Pollit.Infra.Api.AuthenticatedUserProviders;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.Api.Controllers.Polls.GetPollFeed;

[ApiController]
public class GetPollFeedController : OperationControllerBase<GetPollFeedQuery, IGetPollFeedPresenter, GetPollFeedPresenter, GetPollFeedQueryHandler>
{
    public GetPollFeedController(GetPollFeedQueryHandler queryHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(queryHandler, authenticatedUserProvider)
    {
    }
    
    [AllowAnonymous]
    [HttpGet("polls/feed", Name = "GetPollFeed")]
    public async Task<IActionResult?> GetPollFeedAsync([FromQuery] GetPollFeedHttpRequestQueryParams queryParams)
    {
        var query = new GetPollFeedQuery(
            queryParams.OrderBy,
            queryParams.Order,
            queryParams.Author,
            queryParams.CreatedBefore,
            queryParams.CreatedAfter,
            queryParams.PollId,
            new PaginationOptions(queryParams.Page ?? 0, queryParams.PageSize ?? 50));

        var presenter = new GetPollFeedPresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}