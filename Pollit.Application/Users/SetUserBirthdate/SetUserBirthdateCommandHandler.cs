using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Birthdates;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Users.SetUserBirthdate;

public class SetUserBirthdateCommandHandler : CommandHandlerBase<SetUserBirthdateCommand, ISetUserBirthdatePresenter>
{
    private readonly AccountSettingsService _accountSettingsService;
    private readonly IUnitOfWork _unitOfWork;

    public SetUserBirthdateCommandHandler(AccountSettingsService accountSettingsService, IUnitOfWork unitOfWork)
    {
        _accountSettingsService = accountSettingsService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedCommand<SetUserBirthdateCommand> authorizedCommand, ISetUserBirthdatePresenter presenter)
    {
        var birthDate = new Birthdate(authorizedCommand.Command.Year, authorizedCommand.Command.Month, authorizedCommand.Command.Day);
        var result = await _accountSettingsService.SetUserBirthdate(authorizedCommand.Command.UserId, birthDate);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();

                presenter.Success();
            },
            userDoesNotExistError => presenter.UserNotFound(),
            birthdateIsInTheFutureError => presenter.BirthdateIsInTheFuture()
        );
    }

    protected override Task<bool> IsAuthorized(UserId? userId, SetUserBirthdateCommand command) => Task.FromResult(userId is not null && userId == command.UserId);
}