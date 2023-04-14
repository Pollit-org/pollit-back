namespace Pollit.Application.Polls.GetPollFeed;

public class GetPollFeedQueryResultItem
{
    public Guid PollId { get; set; }
    public string Title { get; set; }
    public string[] Options { get; set; }
    public int TotalVotesCount { get; set; }
    public string[] Tags { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}