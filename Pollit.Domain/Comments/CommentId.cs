using Pollit.SeedWork;

namespace Pollit.Domain.Comments;

public class CommentId : IdValueBase
{
    public CommentId(Guid value) : base(value) { }

    public static CommentId NewCommentThreadId() => new(Guid.NewGuid());
    
    public static implicit operator CommentId(Guid commentThreadId) => new (commentThreadId);
    public static implicit operator Guid(CommentId commentId) => commentId.Value;
}