using Pollit.Application.Polls.GetPollResults;

namespace Pollit.Infra.Api.Controllers.Polls.GetPollResults;

public class GetPollResultsPresenter : BasePresenter, IGetPollResultsPresenter
{
    public void Success(PollResults pollResults)
        => Ok(pollResults);
}