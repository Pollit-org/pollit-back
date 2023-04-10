using Newtonsoft.Json;
using Pollit.Infra.Api.Controllers.Polls.CreatePoll;

namespace Pollit.Test.Common.Builders.Api.Controllers.Polls.CreatePoll;

public class CreatePollHttpRequestBodyBuilder : IFluentBuilder<CreatePollHttpRequestBody>
{
    private CreatePollHttpRequestBody _requestBody = new()
    {
        Title = "Pilule rouge ou bleu ?",
        Options = new [] { "Rouge", "Bleu" },
        Tags = new[] { "Matrix", "Cinema" }
    };
    
    public CreatePollHttpRequestBodyBuilder WithTitle(string title)
    {
        _requestBody.Title = title;
        return this;
    }
    
    
    public CreatePollHttpRequestBodyBuilder WithOptions(string[] options)
    {
        _requestBody.Options = options;
        return this;
    }
    

    public CreatePollHttpRequestBodyBuilder WithTags(string[] tags)
    {
        _requestBody.Tags = tags;
        return this;
    }

    public CreatePollHttpRequestBody Build()
    {
        return JsonConvert.DeserializeObject<CreatePollHttpRequestBody>(JsonConvert.SerializeObject(_requestBody));
    }
}