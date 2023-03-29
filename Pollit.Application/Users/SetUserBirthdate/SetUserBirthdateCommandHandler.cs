using Pollit.Domain._Ports;
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

    protected override async Task HandleInternalAsync(SetUserBirthdateCommand command, ISetUserBirthdatePresenter presenter)
    {
        var birthDate = new Birthdate(command.Year, command.Month, command.Day);
        var result = await _accountSettingsService.SetUserBirthdate(command.UserId, birthDate);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();

                presenter.Success();
            },
            userDoesNotExistError => presenter.UserNotFound()
        );
    }
}