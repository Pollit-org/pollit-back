using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Users.SetPermanentUserName;

public class SetPermanentUserNameCommandHandler : CommandHandlerBase<SetPermanentUserNameCommand, ISetPermanentUserNamePresenter>
{
    private readonly AccountSettingsService _accountSettingsService;
    private readonly IUnitOfWork _unitOfWork;

    public SetPermanentUserNameCommandHandler(IUnitOfWork unitOfWork, AccountSettingsService accountSettingsService)
    {
        _unitOfWork = unitOfWork;
        _accountSettingsService = accountSettingsService;
    }

    protected override async Task HandleAsync(AuthorizedCommand<SetPermanentUserNameCommand> command, ISetPermanentUserNamePresenter presenter)
    {
        var result = await _accountSettingsService.SetPermanentUserNameAsync(command.Command.UserId, command.Command.UserName);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();

                presenter.Success();
            },
            userDoesNotExistError => presenter.UserNotFound(),
            userNameIsAlreadyPermanentError => presenter.UsernameIsAlreadyPermanent(),
            userNameAlreadyExistsError => presenter.UserNameAlreadyExists()
        );
    }

    protected override Task<bool> IsAuthorized(UserId? userId, SetPermanentUserNameCommand command) => Task.FromResult(userId is not null && userId == command.UserId);
}