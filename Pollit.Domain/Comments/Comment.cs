using Pollit.Domain.Polls;
using Pollit.Domain.Users;

namespace Pollit.Domain.Comments;

public class Comment
{
    public Comment(CommentId id, PollId pollId, CommentId? parentCommentId, UserId authorId, CommentBody body, CommentVote votes, DateTime createdAt, DateTime? deletedAt)
    {
        Id = id;
        PollId = pollId;
        ParentCommentId = parentCommentId;
        AuthorId = authorId;
        Body = body;
        Votes = votes;
        CreatedAt = createdAt;
        DeletedAt = deletedAt;
    }

    public CommentId Id { get; }
    
    public PollId PollId { get; }
    
    public CommentId? ParentCommentId { get; }
    
    public UserId AuthorId { get; }
    
    public CommentBody Body { get; }
    
    public CommentVote Votes { get; }
    
    public DateTime CreatedAt { get; }
    
    public DateTime? DeletedAt { get; }
}