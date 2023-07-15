namespace Pollit.Application.Polls.GetPollResults;

public class PollResults
{
    public Guid PollId { get; set; }
    
    public int TotalVotesCount => Options.Sum(o => o.VotesCount);

    public List<PollOptionResults> Options { get; set; } = new List<PollOptionResults>();
}