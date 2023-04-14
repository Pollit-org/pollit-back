namespace Pollit.SeedWork.Querying.Pagination;

public class PaginationResult<TItem>
{
    public PaginationResult(IEnumerable<TItem> items, PaginationOptions options, uint count, uint? totalCount)
    {
        Items = items;
        Options = options;
        Count = count;
        TotalCount = totalCount;
    }
    
    public IEnumerable<TItem> Items { get; }

    public PaginationOptions Options { get; }
    
    public uint Count { get; set; }
    
    public uint? TotalCount { get; set; }

    public uint? NextPageNumber => HasNextPage ? Options.PageNumber + 1 : null;
    public uint? PreviousPageNumber => HasPreviousPage ? Options.PageNumber - 1 : null;
    
    public bool HasNextPage => (Options.PageNumber + 1) * Options.PageSize < (TotalCount ?? int.MaxValue);
    public bool HasPreviousPage => Options.PageNumber > 0;
}