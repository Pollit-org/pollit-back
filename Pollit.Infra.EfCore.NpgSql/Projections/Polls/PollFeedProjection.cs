using Microsoft.EntityFrameworkCore;
using Pollit.Application.Polls.GetPollFeed;
using Pollit.Domain.Users;
using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.EfCore.NpgSql.Projections.Polls;

public class PollFeedProjection : IPollFeedProjection
{
    private readonly PollitDbContext _context;
    
    public PollFeedProjection(PollitDbContext context)
    {
        _context = context;
    }

    public PaginationResult<GetPollFeedQueryResultItem> GetPollFeed(GetPollFeedQuery query, UserId? requestingUserId)
    {
        var dbQuery = _context.PollFeedItems.FromSql(@$"select (""Polls.GetPollFeedOfUser""({requestingUserId?.ToString()})).*");
        if (query.Author is not null)
            dbQuery = dbQuery.Where(x => x.Author == query.Author);
        if (query.CreatedBefore is not null)
            dbQuery = dbQuery.Where(x => x.CreatedAt <= query.CreatedBefore);
        if (query.CreatedAfter is not null)
            dbQuery = dbQuery.Where(x => x.CreatedAt >= query.CreatedAfter);
        
        dbQuery = query.OrderBy switch
        {
            null => dbQuery,
            GetPollFeedQueryOrderBy.CreatedAt => dbQuery.OrderBy(x => x.CreatedAt, query.Order!.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(query.OrderBy))
        };
        

        return dbQuery.Paginate(query.PaginationOptions);
    }
}