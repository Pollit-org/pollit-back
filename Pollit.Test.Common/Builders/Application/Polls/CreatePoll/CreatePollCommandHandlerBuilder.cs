using Pollit.Application.Polls.CreatePoll;
using Pollit.Domain._Ports;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Application.Polls.CreatePoll;

public class CreatePollCommandHandlerBuilder
{
    private readonly InMemoryDatabase _inMemoryDatabase = new();

    public CreatePollCommandHandlerBuilder(InMemoryDatabase? inMemoryDatabase = null)
    {
        _inMemoryDatabase = inMemoryDatabase ?? _inMemoryDatabase; }

    public CreatePollCommandHandler Build()
    {
        return new CreatePollCommandHandler(_inMemoryDatabase.GetPollRepository(), _inMemoryDatabase.GetUnitOfWork());
    }
}