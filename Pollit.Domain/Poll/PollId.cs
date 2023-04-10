using Pollit.SeedWork;

namespace Pollit.Domain.Poll;

public class PollId : IdValueBase
{
    public PollId(Guid value) : base(value) { }

    public static PollId NewPollId() => new(Guid.NewGuid());
    
    public static implicit operator PollId(Guid pollId) => new (pollId);
    public static implicit operator Guid(PollId pollId) => pollId.Value;
}