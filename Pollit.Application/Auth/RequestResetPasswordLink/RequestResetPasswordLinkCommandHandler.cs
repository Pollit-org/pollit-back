using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.RequestResetPasswordLink;

public class RequestResetPasswordLinkCommandHandler : OperationHandlerBase<RequestResetPasswordLinkCommand, IRequestResetPasswordLinkPresenter>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CredentialsAuthenticationService _credentialsAuthenticationService;

    public RequestResetPasswordLinkCommandHandler(CredentialsAuthenticationService credentialsAuthenticationService, IUnitOfWork unitOfWork)
    {
        _credentialsAuthenticationService = credentialsAuthenticationService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<RequestResetPasswordLinkCommand> command, IRequestResetPasswordLinkPresenter presenter)
    {
        RequestResetPasswordLinkResult result;
        if (command.AuthorizedFor is not null)
            result = await _credentialsAuthenticationService.RequestResetPasswordLink(command.AuthorizedFor);
        else
            result = await _credentialsAuthenticationService.RequestResetPasswordLink(command.Value.Email!);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success();
            },
            userDoesNotExistError => presenter.UserDoesNotExistError()
        );
    }

    protected override Task<bool> IsAuthorized(UserId? userId, RequestResetPasswordLinkCommand command)
    {
        return Task.FromResult(userId is not null || command.Email is not null);
    }
}