using Pollit.Application.Polls.CreatePoll;
using Pollit.Domain.Poll;

namespace Pollit.Infra.Api.Controllers.Polls.CreatePoll;

public class CreatePollPresenter : BasePresenter, ICreatePollPresenter
{
    public void Success(PollId pollId) => Ok(new CreatePollHttpResponse() { PollId = pollId });
    
    public void PollMustHaveAtLeastTwoOptions(string error) => BadRequest(error);
}