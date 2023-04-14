using Pollit.Domain.Users;

namespace Pollit.Domain.Comments;

public class CommentVote
{
    public CommentVote(CommentId commentId, UserId voterId, ECommentVoteDirection direction)
    {
        CommentId = commentId;
        VoterId = voterId;
        Direction = direction;
    }

    public CommentId CommentId { get; }
    
    public UserId VoterId { get; }
    
    public ECommentVoteDirection Direction { get; }
}