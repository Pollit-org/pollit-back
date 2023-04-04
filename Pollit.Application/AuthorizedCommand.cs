using Pollit.Domain.Users;

namespace Pollit.Application;

public class AuthorizedCommand<TCommand> where TCommand : ICommand
{
    internal AuthorizedCommand(TCommand command, UserId? authorizedFor)
    {
        Command = command;
        AuthorizedFor = authorizedFor;
    }
    
    public TCommand Command { get; }
    public UserId? AuthorizedFor { get; }
}