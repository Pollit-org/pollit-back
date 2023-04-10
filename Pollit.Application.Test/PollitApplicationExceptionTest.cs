using System.Reflection;
using FluentAssertions;
using Pollit.SeedWork;
using Xunit;

namespace Pollit.Application.Test;

public class PollitApplicationExceptionTest
{
    [Fact]
    public void When_ConvertingAnyDomainExceptionToApplicationException_Then_ErrorCodeShouldNeverBeUnknown()
    {
        var allDomainExceptions = Assembly.Load("Pollit.Domain").GetTypes().Where(t => t.IsAssignableTo(typeof(PollitDomainException)) && !t.IsAbstract);
        foreach (var domainExceptionType in allDomainExceptions)
        {
            PollitApplicationException.FromDomainException((PollitDomainException) Activator.CreateInstance(domainExceptionType))
                .ErrorCode
                .Should()
                .NotBe(ApplicationError.UnknownError, because: $"{domainExceptionType.Name} should be handled");
        }
    }
}