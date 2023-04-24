using Pollit.Application.Polls.GetPollFeed;
using Pollit.SeedWork.Querying;

namespace Pollit.Infra.Api.Controllers.Polls.GetPollFeed;

public class GetPollFeedHttpRequestQueryParams
{
    public uint? Page { get; set; }
    
    public uint? PageSize { get; set; }
    
    public EGetPollFeedQueryOrderBy? OrderBy { get; set; }
    
    public EQueryOrder? Order { get; set; }
    
    public string? Author { get; set; }
    
    public string[]? Tags { get; set; }
    
    public DateTime? CreatedBefore { get; set; }
    
    public DateTime? CreatedAfter { get; set; }
    
    public Guid? PollId { get; set; }
}