using Pollit.SeedWork;

namespace Pollit.Domain.Poll;

public class PollOptionId : IdValueBase
{
    public PollOptionId(Guid value) : base(value) { }

    public static PollOptionId NewPollOptionId() => new(Guid.NewGuid());
    
    public static implicit operator PollOptionId(Guid pollOptionId) => new (pollOptionId);
    public static implicit operator Guid(PollOptionId pollOptionId) => pollOptionId.Value;
}