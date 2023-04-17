using Pollit.SeedWork;

namespace Pollit.Domain.Comments;

public class CommentId : IdValueBase
{
    public CommentId(Guid value) : base(value) { }

    public static CommentId NewCommentId() => new(Guid.NewGuid());
    
    public static implicit operator CommentId(Guid commentId) => new (commentId);
    public static implicit operator Guid(CommentId commentId) => commentId.Value;
}