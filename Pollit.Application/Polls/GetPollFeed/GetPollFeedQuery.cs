using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Application.Polls.GetPollFeed;

[OperationAuthorizedForAnyone]
public class GetPollFeedQuery : IOperation
{
    public GetPollFeedQuery(EGetPollFeedQueryOrderBy? orderBy, EQueryOrder? order, string? searchText, string? author, DateTime? createdBefore, DateTime? createdAfter, Guid? pollId, IEnumerable<string>? tags, PaginationOptions paginationOptions)
    {
        OrderBy = orderBy;
        Order = order;
        if (OrderBy is not null && Order is null)
        {
            Order = EQueryOrder.Ascending;
        }

        SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText.Trim(); 
        Author = author;
        CreatedBefore = createdBefore;
        CreatedAfter = createdAfter;
        PollId = pollId;
        Tags = tags ?? Array.Empty<string>();
        PaginationOptions = paginationOptions;
    }
    
    public EGetPollFeedQueryOrderBy? OrderBy { get; }
    
    public EQueryOrder? Order { get; }

    public string? SearchText { get; set; }
    
    public string? Author { get; }
    
    public DateTime? CreatedBefore { get; }
    
    public DateTime? CreatedAfter { get; }
    
    public Guid? PollId { get; }
    
    public IEnumerable<string> Tags { get; set; }

    public PaginationOptions PaginationOptions { get; }
}