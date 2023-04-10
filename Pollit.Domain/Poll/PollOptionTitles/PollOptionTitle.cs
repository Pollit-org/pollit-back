using Pollit.Domain.Poll.PollOptionTitles.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Domain.Poll.PollOptionTitles;

public class PollOptionTitle : StringValueBase
{
    public const int MaxLength = 55;
    
    public PollOptionTitle(string value) : base(value, caseSensitive: true)
    {
        if (value.Length > MaxLength)
            throw new PollOptionTitleTooLongException();
    }
    
    public static implicit operator PollOptionTitle(string pollOption) => new (pollOption);
    public static implicit operator string(PollOptionTitle pollOptionTitle) => pollOptionTitle.Value;
}