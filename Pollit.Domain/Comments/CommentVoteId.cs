using Pollit.SeedWork;

namespace Pollit.Domain.Comments;

public class CommentVoteId : IdValueBase
{
    public CommentVoteId(Guid value) : base(value) { }

    public static CommentVoteId NewCommentVoteId() => new(Guid.NewGuid());
    
    public static implicit operator CommentVoteId(Guid commentVoteId) => new (commentVoteId);
    public static implicit operator Guid(CommentVoteId commentVoteId) => commentVoteId.Value;
}