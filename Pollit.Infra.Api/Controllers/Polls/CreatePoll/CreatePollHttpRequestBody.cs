namespace Pollit.Infra.Api.Controllers.Polls.CreatePoll;

public class CreatePollHttpRequestBody
{
    public string Title { get; set; }
    
    public string[] Tags { get; set; }
    
    public string[] Options { get; set; }
}