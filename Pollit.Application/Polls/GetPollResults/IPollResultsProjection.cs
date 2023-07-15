namespace Pollit.Application.Polls.GetPollResults;

public interface IPollResultsProjection
{
    Task<PollResults> GetPollResultsAsync(GetPollResultsQuery query);
}