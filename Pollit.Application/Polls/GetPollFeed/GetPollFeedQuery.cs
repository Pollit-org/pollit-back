using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

[OperationAuthorizedForAnyone]
public class GetPollFeedQuery : IOperation
{
    public GetPollFeedQuery(GetPollFeedQueryOrderBy? orderBy, QueryOrder? order, string? author, DateTime? createdBefore, DateTime? createdAfter, PaginationOptions paginationOptions)
    {
        OrderBy = orderBy;
        if (OrderBy is not null && Order is null)
        {
            Order = QueryOrder.Ascending;
        }
        Order = order;
        Author = author;
        CreatedBefore = createdBefore;
        CreatedAfter = createdAfter;
        PaginationOptions = paginationOptions;
    }

    public GetPollFeedQueryOrderBy? OrderBy { get; }
    
    public QueryOrder? Order { get; }

    public string? Author { get; }
    
    public DateTime? CreatedBefore { get; }
    
    public DateTime? CreatedAfter { get; }
    
    public PaginationOptions PaginationOptions { get; }
}