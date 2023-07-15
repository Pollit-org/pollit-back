namespace Pollit.Application.Polls.GetPollResults;

public class GetPollResultsQuery : IOperation
{
    public GetPollResultsQuery(Guid pollId, int ageGranularity, AgeIntervalsDistribution intervalsDistribution)
    {
        PollId = pollId;
        AgeGranularity = ageGranularity;
        IntervalsDistribution = intervalsDistribution;
    }

    public Guid PollId { get; }
    
    public int AgeGranularity { get; }
    
    public AgeIntervalsDistribution IntervalsDistribution { get; } 

    public enum AgeIntervalsDistribution
    {
        EvenRanges, // example : 0-10, 11-20, 21-30, etc. 
        EvenDemography // under 18, 18-25, 25-35, etc.
    }
}