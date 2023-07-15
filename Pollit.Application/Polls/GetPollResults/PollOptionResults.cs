namespace Pollit.Application.Polls.GetPollResults;

public class PollOptionResults
{
    public string Title { get; set; }

    public int VotesCount => VotesPerFeature.FirstOrDefault()!.Intervals.Sum(i => i.VotesCount);
    
    public List<PollOptionFeatureResults> VotesPerFeature { get; set; }
}