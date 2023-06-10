namespace Pollit.Application.Polls.GetPollFeed;

public class GetPollFeedQueryResultItem
{
    public double SearchRank { get; set; }
    public Guid PollId { get; set; }
    public string Title { get; set; }
    public bool HasMyVote { get; set; }
    public GetPollFeedQueryResultItemOption[] Options { get; set; }
    public int TotalVotesCount { get; set; }
    public int CommentCount { get; set; }
    public string[] Tags { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GetPollFeedQueryResultItemOption
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int? VotesCount { get; set; }
    public bool? HasMyVote { get; set; }
}