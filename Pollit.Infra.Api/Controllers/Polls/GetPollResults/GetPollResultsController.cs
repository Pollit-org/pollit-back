using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Polls.GetPollResults;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Polls.GetPollResults;

[ApiController]
public class GetPollResultsController : OperationControllerBase<GetPollResultsQuery, IGetPollResultsPresenter, GetPollResultsPresenter, GetPollResultsQueryHandler>
{
    public GetPollResultsController(GetPollResultsQueryHandler queryHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(queryHandler, authenticatedUserProvider)
    {
    }
    
    [AllowAnonymous]
    [HttpGet("polls/{pollId:guid}/results", Name = "GetPollResults")]
    public async Task<IActionResult?> GetPollResultsAsync([FromRoute] Guid pollId, [FromQuery] GetPollResultsQueryParams queryParams)
    {
        var query = new GetPollResultsQuery(pollId, queryParams.AgeGranularity, GetPollResultsQuery.AgeIntervalsDistribution.EvenDemography);

        var presenter = new GetPollResultsPresenter();

        await HandleOperationAsync(query, presenter);
        
        return presenter.ActionResult;
    }
}