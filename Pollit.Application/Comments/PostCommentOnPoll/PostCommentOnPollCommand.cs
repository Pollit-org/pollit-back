namespace Pollit.Application.Comments.PostCommentOnPoll;

public class PostCommentOnPollCommand : IOperation
{
    public PostCommentOnPollCommand(Guid pollId, Guid? parentCommentId, string body)
    {
        PollId = pollId;
        ParentCommentId = parentCommentId;
        Body = body;
    }

    public Guid PollId { get; }
    
    public Guid? ParentCommentId { get; }
    
    public string Body { get; }
}