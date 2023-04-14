namespace Pollit.SeedWork.Querying.Pagination;

public class PaginationOptions
{
    public PaginationOptions(uint pageNumber, uint pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public uint PageNumber { get; set; }
    public uint PageSize { get; set; }
}