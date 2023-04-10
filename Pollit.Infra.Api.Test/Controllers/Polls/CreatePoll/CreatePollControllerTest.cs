using System.Net;
using FluentAssertions.AspNetCore.Mvc;
using Pollit.Domain.Poll;
using Pollit.Infra.Api.Controllers.Polls.CreatePoll;
using Pollit.Infra.Api.Test.x_FluentAssertionExtensions;
using Pollit.Test.Common.Builders.Api;
using Pollit.Test.Common.Builders.Api.Controllers.Polls.CreatePoll;
using Pollit.Test.Common.Builders.Application.Polls.CreatePoll;
using Xunit;

namespace Pollit.Infra.Api.Test.Controllers.Polls.CreatePoll;

public class CreatePollControllerTest
{
    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("ab")]
    public async Task When_ICreateAPollWithATitleThatIsLessThanThreeCharactersLong_Then_IShouldReceiveTitleTooShortError(string tooShortTitle)
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        var requestBody = new CreatePollHttpRequestBodyBuilder().WithTitle(tooShortTitle).Build();
        
        var response = await controller.CreatePollAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:POLLS:TITLE_TOO_SHORT");
    }
    
    [Theory]
    [InlineData("Le houmous vous le preferez avec des graissins, avec du pain, ou avec des chips ? Parceque moi perso je prend avec du pain !!!")] // 126 char
    public async Task When_ICreateAPollWithATitleThatIsMoreThan125CharactersLong_Then_IShouldReceiveTitleTooLongError(string tooLongTitle)
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        var requestBody = new CreatePollHttpRequestBodyBuilder().WithTitle(tooLongTitle).Build();
        
        var response = await controller.CreatePollAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:POLLS:TITLE_TOO_LONG");
    }
    
    [Fact]
    public async Task When_ICreateAPollWithoutOptions_Then_IShouldReceiveOptionsCountTooLowError()
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        var requestBody = new CreatePollHttpRequestBodyBuilder().WithOptions(Array.Empty<string>()).Build();
        
        var response = await controller.CreatePollAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:POLLS:OPTIONS_COUNT_SHOULD_BE_HIGHER_THAN_TWO");
    }
    
    [Fact]
    public async Task When_ICreateAPollWithOnlyOneOption_Then_IShouldReceiveOptionsCountTooLowError()
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        var requestBody = new CreatePollHttpRequestBodyBuilder().WithOptions(new []{"Toto"}).Build();
        
        var response = await controller.CreatePollAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:POLLS:OPTIONS_COUNT_SHOULD_BE_HIGHER_THAN_TWO");
    }
    
    [Fact]
    public async Task When_ICreateAPollWithAnOptionThatIsLongerThan55Character_Then_IShouldReceiveOptionTooLongError()
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        // 56 chars
        var requestBody = new CreatePollHttpRequestBodyBuilder().WithOptions(new [] { "This option is very loooooooooooooooooooooooooooooonooog" }).Build();
        
        var response = await controller.CreatePollAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:POLLS:OPTION_TITLE_TOO_LONG");
    }
    
    [Fact]
    public async Task When_ICreateAPollThatIsValid_Then_IShouldReceiveTheIdOfTheCreatedPoll()
    {
        var commandHandler = new CreatePollCommandHandlerBuilder().Build();
        
        var controller = new CreatePollControllerBuilder().WithCommandHandler(commandHandler).WithAuthenticatedUser<CreatePollController, CreatePollControllerBuilder>().Build();
    
        var requestBody = new CreatePollHttpRequestBodyBuilder().Build();
        
        var response = await controller.CreatePollAsync(requestBody);

        response.Should().BeOkObjectResult().WithValueMatch<CreatePollHttpResponse>(response => response.PollId != default);
    }
}