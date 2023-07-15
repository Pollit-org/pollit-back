namespace Pollit.Application.Polls.GetPollResults;

public interface IGetPollResultsPresenter : IPresenter
{
    void Success(PollResults pollResults);
}