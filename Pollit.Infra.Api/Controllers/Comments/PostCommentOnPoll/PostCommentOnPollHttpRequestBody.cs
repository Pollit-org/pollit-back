namespace Pollit.Infra.Api.Controllers.Comments.PostCommentOnPoll;

public class PostCommentOnPollHttpRequestBody
{
    public Guid? ParentCommentId { get; set; }
    public string CommentBody { get; set; }
}