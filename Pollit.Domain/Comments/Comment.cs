using Pollit.Domain.Polls;
using Pollit.Domain.Users;

namespace Pollit.Domain.Comments;

public class Comment
{
    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    private Comment() { }
    
    public Comment(CommentId id, PollId pollId, CommentId? parentCommentId, UserId authorId, CommentBody body, IEnumerable<CommentVote> votes, DateTime createdAt, DateTime? deletedAt)
    {
        Id = id;
        PollId = pollId;
        ParentCommentId = parentCommentId;
        AuthorId = authorId;
        Body = body;
        _votes = votes.ToList();
        CreatedAt = createdAt;
        DeletedAt = deletedAt;
    }

    internal static Comment NewComment(PollId pollId, CommentId? parentCommentId, UserId authorId, CommentBody body)
        => new(CommentId.NewCommentId(), pollId, parentCommentId, authorId, body, Enumerable.Empty<CommentVote>(), DateTime.UtcNow, null);

    public CommentId Id { get; protected set; }
    
    public PollId PollId { get; protected set; }
    
    public CommentId? ParentCommentId { get; protected set; }
    
    public UserId AuthorId { get; protected set; }
    
    public CommentBody Body { get; protected set; }
    
    private readonly IList<CommentVote> _votes;
    public IReadOnlyCollection<CommentVote> Votes => _votes.AsReadOnly();

    public DateTime CreatedAt { get; protected set; }
    
    public DateTime? DeletedAt { get; protected set; }

    public bool IsDeleted => DeletedAt != null;
}
