using Pollit.SeedWork;

namespace Pollit.Domain.Poll;

public class PollOptionVoteId : IdValueBase
{
    public PollOptionVoteId(Guid value) : base(value) { }

    public static PollOptionVoteId NewPollOptionVoteId() => new(Guid.NewGuid());
    
    public static implicit operator PollOptionVoteId(Guid pollOptionVoteId) => new (pollOptionVoteId);
    public static implicit operator PollOptionVoteId(PollId pollOptionVoteId) => pollOptionVoteId.Value;
}