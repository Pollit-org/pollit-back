namespace Pollit.Application;

public partial class ApplicationError
{
    private const string PollsErrorPrefix = $"{GlobalErrorPrefix}:POLLS";

    public const string PollTitleTooShort = $"{PollsErrorPrefix}:TITLE_TOO_SHORT";
    public const string PollTitleTooLong = $"{PollsErrorPrefix}:TITLE_TOO_LONG";
    public const string PollMustHaveAtLeastTwoOptions = $"{PollsErrorPrefix}:OPTIONS_COUNT_SHOULD_BE_HIGHER_THAN_TWO";
    public const string PollOptionTitleTooLong = $"{PollsErrorPrefix}:OPTION_TITLE_TOO_LONG";
    public const string PollNotFound = $"{PollsErrorPrefix}:POLL_NOT_FOUND";
    public const string PollOptionNotFound = $"{PollsErrorPrefix}:OPTIONS:OPTION_NOT_FOUND";
    public const string UserHasAlreadyVotedForThisPoll = $"{PollsErrorPrefix}:USER_HAS_ALREADY_VOTED_FOR_THIS_POLL";
}