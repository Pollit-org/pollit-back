using Pollit.Domain.Polls.PollTitles.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Domain.Polls.PollTitles;

public class PollTitle : StringValueBase
{
    public const int MinLength = 3;
    public const int MaxLength = 125;
    
    public PollTitle(string value) : base(value, caseSensitive: true)
    {
        switch (value.Length)
        {
            case < MinLength:
                throw new PollTitleTooShortException();
            case > MaxLength:
                throw new PollTitleTooLongException();
        }
    }
    
    public static implicit operator PollTitle(string pollTitle) => new (pollTitle);
    public static implicit operator string(PollTitle pollTitle) => pollTitle.Value;
}