using System.Threading.Tasks;
using Pollit.Application._Ports;
using Pollit.Domain.Users;

namespace Pollit.Application.Users.SetPermanentUserName;

public class SetPermanentUserNameCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetPermanentUserNameCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(SetPermanentUserNameCommand command, ISetPermanentUserNamePresenter presenter)
    {
        var user = await _userRepository.GetAsync(command.UserId);
        if (user is null)
        {
            presenter.UserNotFound();
            return;
        }

        if (user.HasPermanentUserName)
        {
            presenter.UsernameIsAlreadyPermanent();
            return;
        }

        if (await _userRepository.UserNameExistsAsync(command.UserName))
        {
            presenter.UserNameAlreadyExists();
            return;
        }

        user.SetPermanentUserName(command.UserName);

        await _unitOfWork.SaveChangesAsync();
        
        presenter.Success();
    }
}