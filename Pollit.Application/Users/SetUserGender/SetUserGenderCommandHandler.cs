using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Users.SetUserGender;

public class SetUserGenderCommandHandler : OperationHandlerBase<SetUserGenderCommand, ISetUserGenderPresenter>
{
    private readonly AccountSettingsService _accountSettingsService;
    private readonly IUnitOfWork _unitOfWork;

    public SetUserGenderCommandHandler(IUnitOfWork unitOfWork, AccountSettingsService accountSettingsService)
    {
        _unitOfWork = unitOfWork;
        _accountSettingsService = accountSettingsService;
    }

    protected override async Task HandleAsync(AuthorizedOperation<SetUserGenderCommand> authorizedCommand, ISetUserGenderPresenter presenter)
    {
        var result = await _accountSettingsService.SetUserGender(authorizedCommand.Value.UserId, authorizedCommand.Value.Gender);

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