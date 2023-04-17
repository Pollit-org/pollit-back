namespace Pollit.Application;

public partial class ApplicationError
{
    private const string CommentsErrorPrefix = $"{GlobalErrorPrefix}:COMMENTS";

    public const string CommentNotFound = $"{CommentsErrorPrefix}:COMMENT_NOT_FOUND";
    public const string ParentCommentIsDeleted = $"{CommentsErrorPrefix}:PARENT_COMMENT_IS_DELETED";
}