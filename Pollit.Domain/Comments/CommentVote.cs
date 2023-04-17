using Pollit.Domain.Users;

namespace Pollit.Domain.Comments;

public class CommentVote
{
    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    private CommentVote() { }
    
    private CommentVote(CommentVoteId id, UserId voterId, ECommentVoteDirection direction)
    {
        Id = id;
        VoterId = voterId;
        Direction = direction;
    }

    public CommentVoteId Id { get; protected set; }
    
    public UserId VoterId { get; protected set; }
    
    public ECommentVoteDirection Direction { get; protected set; }
}