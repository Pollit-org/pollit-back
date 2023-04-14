using Pollit.SeedWork;

namespace Pollit.Domain.Comments;

public class CommentBody : StringValueBase
{
    public CommentBody(string value) : base(value, caseSensitive: true)
    {
    }
    
    public static implicit operator CommentBody(string commentBody) => new (commentBody);
    public static implicit operator string(CommentBody commentBody) => commentBody.Value;
}