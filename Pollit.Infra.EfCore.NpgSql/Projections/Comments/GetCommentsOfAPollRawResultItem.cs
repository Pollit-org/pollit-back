using Pollit.Domain.Comments;

namespace Pollit.Infra.EfCore.NpgSql.Projections.Comments;

public class GetCommentsOfAPollRawResultItem
{
    public Guid Id { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Author { get; set; }
    public string Body { get; set; }
    public int UpVotesCount { get; set; }
    public int DownVotesCount { get; set; }
    public ECommentVoteDirection? MyVoteDirection { get; set; }
    public int Depth { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
}