namespace Pollit.Infra.Api.Controllers.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollHttpRequestQueryParams
{
    public Guid? RootCommentId { get; set; }
    public int? MaxRecursiveDepth { get; set; }
}