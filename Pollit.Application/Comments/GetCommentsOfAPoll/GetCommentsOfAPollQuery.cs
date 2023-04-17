using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Comments.GetCommentsOfAPoll;

public class GetCommentsOfAPollQuery : IOperation
{
    public GetCommentsOfAPollQuery(Guid pollId, EGetCommentsOfAPollOrderBy? orderBy, EQueryOrder? order, PaginationOptions paginationOptions, Guid? rootCommentId, int maxRecursiveDepth = 3)
    {
        OrderBy = orderBy;
        Order = order;
        if (OrderBy is not null && Order is null)
        {
            Order = EQueryOrder.Ascending;
        }
        PollId = pollId;
        PaginationOptions = paginationOptions;
        RootCommentId = rootCommentId;
        MaxRecursiveDepth = maxRecursiveDepth;
    }

    public Guid PollId { get; }
    
    public EGetCommentsOfAPollOrderBy? OrderBy { get; }
    
    public EQueryOrder? Order { get; }

    public int MaxRecursiveDepth { get; set; }
    
    public Guid? RootCommentId { get; set; }

    public PaginationOptions PaginationOptions { get; }
}