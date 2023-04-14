namespace Pollit.Application.Polls.CastVoteToPoll;

public class CastVoteToPollCommand : IOperation
{
    public CastVoteToPollCommand(Guid voterId, Guid pollId, Guid optionId)
    {
        VoterId = voterId;
        PollId = pollId;
        OptionId = optionId;
    }

    [OperationAuthorizedFor]
    public Guid VoterId { get; }
    
    public Guid PollId { get; }
    
    public Guid OptionId { get; }
}