using Pollit.Domain.Polls;

namespace Pollit.Application.Polls.CreatePoll;

public interface ICreatePollPresenter : IPresenter
{
    void Success(PollId pollId);
    void PollMustHaveAtLeastTwoOptions(string error = ApplicationError.PollMustHaveAtLeastTwoOptions);
}