using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Users.SetUserGender;

public class SetUserGenderCommandHandler : CommandHandlerBase<SetUserGenderCommand, ISetUserGenderPresenter>
{
    private readonly AccountSettingsService _accountSettingsService;
    private readonly IUnitOfWork _unitOfWork;

    public SetUserGenderCommandHandler(IUnitOfWork unitOfWork, AccountSettingsService accountSettingsService)
    {
        _unitOfWork = unitOfWork;
        _accountSettingsService = accountSettingsService;
    }

    protected override async Task HandleInternalAsync(SetUserGenderCommand command, ISetUserGenderPresenter presenter)
    {
        var result = await _accountSettingsService.SetUserGender(command.UserId, command.Gender);

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