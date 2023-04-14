using Pollit.SeedWork;

namespace Pollit.Domain.Polls;

public class PollTag : StringValueBase
{
    public PollTag(string value) : base(value, caseSensitive: false)
    {
    }
    
    public static implicit operator PollTag(string pollTag) => new (pollTag);
    public static implicit operator string(PollTag pollTag) => pollTag.Value;
}