using System.Linq.Expressions;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.SeedWork.Querying;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T, TKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> selector, EQueryOrder order)
    {
        return order switch
        {
            EQueryOrder.Ascending => queryable.OrderBy(selector),
            EQueryOrder.Descending => queryable.OrderByDescending(selector),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };
    }
    
    public static PaginationResult<T> Paginate<T>(this IQueryable<T> queryable, PaginationOptions options, bool countTotal = true)
    {
        int? totalCount = countTotal ? queryable.Count() : null;
        var paginatedQueryable = queryable.Skip((int) (options.PageNumber * options.PageSize)).Take((int) options.PageSize);
        
        return new PaginationResult<T>(paginatedQueryable, options, (uint) paginatedQueryable.Count(), (uint?) totalCount);
    }
}