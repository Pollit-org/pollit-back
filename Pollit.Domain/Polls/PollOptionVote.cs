using Pollit.Domain.Users;

namespace Pollit.Domain.Polls;

public class PollOptionVote
{
    private PollOptionVote(PollOptionVoteId id, UserId voterId, DateTime votedAt)
    {
        Id = id;
        VoterId = voterId;
        VotedAt = votedAt;
    }

    public static PollOptionVote NewVote(UserId voterId, DateTime? votedAt = null) 
        => new (PollOptionVoteId.NewPollOptionVoteId(), voterId, votedAt ?? DateTime.UtcNow);

    public PollOptionVoteId Id { get; protected set; }

    public UserId VoterId { get; protected set; }
    
    public DateTime VotedAt { get; protected set; }
}