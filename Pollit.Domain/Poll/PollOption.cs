using Pollit.Domain.Poll.PollOptionTitles;
using Pollit.Domain.Users;

namespace Pollit.Domain.Poll;

public class PollOption
{
    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    private PollOption() { }
    
    public PollOption(PollOptionId id, PollOptionTitle title, IEnumerable<PollOptionVote> votes)
    {
        Title = title;
        _votes = votes.ToList();
        Id = id;
    }

    internal static PollOption NewPollOption(PollOptionTitle title)
    {
        return new PollOption(PollOptionId.NewPollOptionId(), title, new List<PollOptionVote>());
    }

    public PollOptionId Id { get; protected set; }

    public PollOptionTitle Title { get; protected set; }

    private readonly IList<PollOptionVote> _votes;
    public IReadOnlyCollection<PollOptionVote> Votes => _votes.AsReadOnly();

    internal void AddVote(UserId userId)
    {
        _votes.Add(PollOptionVote.NewVote(userId, DateTime.UtcNow));
    }
}