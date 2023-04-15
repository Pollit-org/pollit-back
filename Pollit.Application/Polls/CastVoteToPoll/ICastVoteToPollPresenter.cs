namespace Pollit.Application.Polls.CastVoteToPoll;

public interface ICastVoteToPollPresenter : IPresenter
{
    void Success();
    
    void VoterNotFound(string error = ApplicationError.UserNotFound);
    
    void PollNotFound(string error = ApplicationError.PollNotFound);
    
    void OptionNotFound(string error = ApplicationError.PollOptionNotFound);
    void UserHasAlreadyVoted(string error = ApplicationError.UserHasAlreadyVotedForThisPoll);
}