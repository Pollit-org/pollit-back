namespace Pollit.Application.Polls.GetPollResults;

public class PollOptionFeatureResults
{
    public string Name { get; set; }

    public List<PollOptionFeatureResultsInterval> Intervals { get; set; }
}