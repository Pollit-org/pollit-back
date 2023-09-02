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
        var dbQuery = _context.PollFeedItems.FromSql(BuildBaseQuery(requestingUserId, query.SearchText));
        if (query.Author is not null)
            dbQuery = dbQuery.Where(x => x.Author == query.Author);
        if (query.CreatedBefore is not null)
            dbQuery = dbQuery.Where(x => x.CreatedAt <= query.CreatedBefore);
        if (query.CreatedAfter is not null)
            dbQuery = dbQuery.Where(x => x.CreatedAt >= query.CreatedAfter);
        if (query.PollId is not null)
            dbQuery = dbQuery.Where(x => x.PollId == query.PollId);
        if (query.Tags.Any())
            dbQuery = dbQuery.Where(p => p.Tags.Any(t => query.Tags.ToArray().Contains(t)));
        if (query.SearchText is not null)
            dbQuery = dbQuery.Where(p => p.SearchRank > 0.001);

        dbQuery = query.OrderBy switch
        {
            null when query.SearchText is not null => dbQuery.OrderByDescending(x => x.SearchRank),
            null => dbQuery,
            EGetPollFeedQueryOrderBy.CreatedAt => dbQuery.OrderBy(x => x.CreatedAt, query.Order!.Value),
            EGetPollFeedQueryOrderBy.TotalVotesCount => dbQuery.OrderBy(x => x.TotalVotesCount, query.Order!.Value),
            EGetPollFeedQueryOrderBy.Trending => dbQuery.OrderBy(x => x.TotalVotesCount / (DateTime.UtcNow - x.CreatedAt).TotalMinutes, EQueryOrder.Descending),
            _ => throw new ArgumentOutOfRangeException(nameof(query.OrderBy))
        };

        return dbQuery.Paginate(query.PaginationOptions);
    }

    private FormattableString BuildBaseQuery(UserId? requestingUserId, string? searchText) =>
        $"""
select
    case 
        when length({searchText ?? ""}) > 0 then ts_rank(to_tsvector("Polls"."Title"), plainto_tsquery({searchText ?? ""}))
        else 0
    end as "SearchRank",
    "Polls"."Id" as "PollId",
    "Polls"."Title",
    string_to_array("Polls"."Tags", ',') as "Tags",
    "Polls"."CreatedAt",
    "Options",
    "TotalVotesCount",
    "Users"."UserName" AS "Author",
    "HasMyVote",
    coalesce("CommentCount", 0) as "CommentCount"
from "Polls"
 join (
    select
        "PollId",
        coalesce(bool_or("HasMyVote"), false) as "HasMyVote",
        case when coalesce(bool_or("HasMyVote"), false) 
        then 
            json_agg(
                json_build_object('Id', "Id", 'Title', "Title", 'VotesCount', coalesce("VotesCount", 0), 'HasMyVote', coalesce("HasMyVote", false))
            )
        else
            json_agg(
                json_build_object('Id', "Id", 'Title', "Title", 'VotesCount', null, 'HasMyVote', coalesce("HasMyVote", false))
            )
        end as "Options",
        coalesce(sum("VotesCount"), 0) as "TotalVotesCount"
    from "Polls.Options"
    left join (
        select
            "PollOptionId",
            bool_or("VoterId" = {requestingUserId?.ToString()}) AS "HasMyVote",
            count(*) as "VotesCount"
        from "Polls.Options.Votes"
        group by "PollOptionId"
    ) POV on "Id" = POV."PollOptionId"
    group by "PollId"
) PO on "Polls"."Id" = PO."PollId"
join "Users" on "AuthorId" = "Users"."Id"
left join (
    select "PollId", count(*) as "CommentCount"
    from "Comments"
    group by "PollId"
) Comments on Comments."PollId" = "Polls"."Id"
""";
}