using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

[OperationAuthorizedForAnyone]
public class GetPollFeedQuery : IOperation
{
    public GetPollFeedQuery(EGetPollFeedQueryOrderBy? orderBy, EQueryOrder? order, string? author, DateTime? createdBefore, DateTime? createdAfter, Guid? pollId, PaginationOptions paginationOptions)
    {
        OrderBy = orderBy;
        Order = order;
        if (OrderBy is not null && Order is null)
        {
            Order = EQueryOrder.Ascending;
        }
        Author = author;
        CreatedBefore = createdBefore;
        CreatedAfter = createdAfter;
        PollId = pollId;
        PaginationOptions = paginationOptions;
    }

    public EGetPollFeedQueryOrderBy? OrderBy { get; }
    
    public EQueryOrder? Order { get; }

    public string? Author { get; }
    
    public DateTime? CreatedBefore { get; }
    
    public DateTime? CreatedAfter { get; }
    
    public Guid? PollId { get; }
    
    public PaginationOptions PaginationOptions { get; }
}