using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Birthdates;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Users.SetUserBirthdate;

public class SetUserBirthdateCommandHandler : OperationHandlerBase<SetUserBirthdateCommand, ISetUserBirthdatePresenter>
{
    private readonly AccountSettingsService _accountSettingsService;
    private readonly IUnitOfWork _unitOfWork;

    public SetUserBirthdateCommandHandler(AccountSettingsService accountSettingsService, IUnitOfWork unitOfWork)
    {
        _accountSettingsService = accountSettingsService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<SetUserBirthdateCommand> authorizedCommand, ISetUserBirthdatePresenter presenter)
    {
        var birthDate = new Birthdate(authorizedCommand.Value.Year, authorizedCommand.Value.Month, authorizedCommand.Value.Day);
        var result = await _accountSettingsService.SetUserBirthdate(authorizedCommand.Value.UserId, birthDate);

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
}