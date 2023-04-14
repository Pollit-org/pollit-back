namespace Pollit.Application.Polls.CreatePoll;

public class CreatePollCommand : IOperation
{
    public CreatePollCommand(Guid authorId, string title, string[] tags, string[] options)
    {
        AuthorId = authorId;
        Title = title;
        Tags = tags;
        Options = options;
    }
    
    [OperationAuthorizedFor]
    public Guid AuthorId { get; }

    public string Title { get; }
    
    public string[] Tags { get; }
    
    public string[] Options { get; }
}