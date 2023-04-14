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
    
    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpGet("polls/feed", Name = "GetPollFeed")]
    public async Task<IActionResult?> GetPollFeedAsync([FromQuery] GetPollFeedHttpRequestQueryParams requestQueryParams)
    {
        var query = new GetPollFeedQuery(null, null, null, null, null, new PaginationOptions(0, 50));

        var presenter = new GetPollFeedPresenter();

        await HandleOperationAsync(query, presenter);

        return presenter.ActionResult;
    }
}