using Pollit.Application.Comments.GetCommentsOfAPoll;

namespace Pollit.Infra.EfCore.NpgSql.Projections.Comments;

public static class GetCommentsOfAPollRawResultItemExtensions
{
    public static IEnumerable<GetCommentsOfAPollQueryResultItem> ToQueryResultItems(this IQueryable<GetCommentsOfAPollRawResultItem> rawItems, Guid? rootCommentId)
    {
        var idToQueryResultItem = new Dictionary<Guid, GetCommentsOfAPollQueryResultItem>();
        var rootItemsToReturn = new List<GetCommentsOfAPollQueryResultItem>();
        
        var rawItemsQueue = new Queue<GetCommentsOfAPollRawResultItem>(rawItems);

        while (rawItemsQueue.Count > 0)
        {
            var rawItem = rawItemsQueue.Dequeue();
            if (rawItem.ParentCommentId != null && rawItem.ParentCommentId != rootCommentId && !idToQueryResultItem.ContainsKey(rawItem.ParentCommentId.Value))
            {
                rawItemsQueue.Enqueue(rawItem);
                continue;
            }
            
            var queryResultItem = new GetCommentsOfAPollQueryResultItem
            {
                Id = rawItem.Id,
                ParentCommentId = rawItem.ParentCommentId,
                Author = rawItem.Author,
                Body = rawItem.Body,
                UpVotesCount = rawItem.UpVotesCount,
                DownVotesCount = rawItem.DownVotesCount,
                CreatedAt = rawItem.CreatedAt,
                MyVoteDirection = rawItem.MyVoteDirection,
                Children = new List<GetCommentsOfAPollQueryResultItem>(),
                Depth = rawItem.Depth
            };
            
            if (rawItem.ParentCommentId == rootCommentId)
                rootItemsToReturn.Add(queryResultItem);
            else
                idToQueryResultItem[rawItem.ParentCommentId!.Value].Children.Add(queryResultItem);

            idToQueryResultItem.Add(queryResultItem.Id, queryResultItem);
        }

        return rootItemsToReturn;
    }
}