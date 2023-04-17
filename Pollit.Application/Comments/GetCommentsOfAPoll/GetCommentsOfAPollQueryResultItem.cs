using Pollit.Domain.Comments;

namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollQueryResultItem
{
    public Guid Id { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Body { get; set; }
    public ECommentVoteDirection? MyVoteDirection { get; set; }
    public int UpVotesCount { get; set; }
    public int DownVotesCount { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; } 
    public int Depth { get; set; }
    public List<GetCommentsOfAPollQueryResultItem> Children { get; set; }
}