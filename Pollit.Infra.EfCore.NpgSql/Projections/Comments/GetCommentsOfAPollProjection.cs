using Microsoft.EntityFrameworkCore;
using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.Domain.Comments;
using Pollit.Domain.Polls;
using Pollit.Domain.Users;
using Pollit.SeedWork.Querying;
using Pollit.SeedWork.Querying.Pagination;

namespace Pollit.Infra.EfCore.NpgSql.Projections.Comments;

public class GetCommentsOfAPollProjection : IGetCommentsOfAPollProjection
{
    private readonly PollitDbContext _context;
    
    public GetCommentsOfAPollProjection(PollitDbContext context)
    {
        _context = context;
    }
    
    public PaginationResult<GetCommentsOfAPollQueryResultItem> GetCommentsOfAPoll(GetCommentsOfAPollQuery query, UserId? requestingUserId)
    {
        var dbQuery = _context.CommentOfAPollRawItems.FromSql(BuildBaseQuery(requestingUserId, query.PollId, query.RootCommentId, query.MaxRecursiveDepth));

        dbQuery = query.OrderBy switch
        {
            null => dbQuery,
            EGetCommentsOfAPollOrderBy.CreatedAt => dbQuery.OrderBy(x => x.CreatedAt, query.Order!.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(query.OrderBy))
        };

        var totalCount = dbQuery.Count();
        var dbPageQuery = dbQuery.Skip((int) (query.PaginationOptions.PageNumber * query.PaginationOptions.PageSize)).Take((int) query.PaginationOptions.PageSize);

        return new PaginationResult<GetCommentsOfAPollQueryResultItem>(dbPageQuery.ToQueryResultItems(query.RootCommentId), query.PaginationOptions, (uint) dbPageQuery.Count(), (uint)totalCount);
    }
    
    private FormattableString BuildBaseQuery(UserId? requestingUserId, PollId pollId, CommentId? rootCommentId, int? maxRecursiveDepth) =>
        $"""
WITH RECURSIVE 
    comments_and_votes AS (
    SELECT "PollId", "UserName" AS "Author", "ParentCommentId", "Comments"."Id", CASE WHEN "DeletedAt" is not null THEN '[DELETED]' else "Body" END AS "Body"  , coalesce("UpVotesCount", 0) AS "UpVotesCount", coalesce("DownVotesCount", 0) AS "DownVotesCount", CASE WHEN "HasMyUpVote" THEN 1 WHEN "HasMyDownVote" THEN -1 ELSE null END AS "MyVoteDirection", "Users"."CreatedAt", CASE WHEN "DeletedAt" is not null THEN true else false END "IsDeleted"
    FROM "Comments"
    left join (select "CommentId", count(*) "UpVotesCount", bool_or("VoterId" is not distinct from {requestingUserId?.ToString()}) "HasMyUpVote" from "Comments.Votes" where "Direction" = 1 group by "CommentId") "UpVotes" on "Id" = "UpVotes"."CommentId"
    left join (select "CommentId", count(*) "DownVotesCount", bool_or("VoterId" is not distinct from {requestingUserId?.ToString()}) "HasMyDownVote" from "Comments.Votes" where "Direction" = -1 group by "CommentId") "DownVotes" on "Id" = "DownVotes"."CommentId"
    join "Users" on "AuthorId" = "Users"."Id"
), sub_comments AS (
    SELECT comments_and_votes.*, 0::int AS "Depth" from comments_and_votes
    WHERE 
        1 = 1
        AND "ParentCommentId" is not distinct from {rootCommentId?.ToString()} 
        AND "PollId" = {pollId.ToString()}
  UNION
    SELECT comments_and_votes.*, ("Depth" + 1)::int from comments_and_votes
    JOIN sub_comments sc ON comments_and_votes."ParentCommentId" = sc."Id" 
)
SELECT *
FROM sub_comments
where "Depth" < {maxRecursiveDepth ?? int.MaxValue}
""";
}