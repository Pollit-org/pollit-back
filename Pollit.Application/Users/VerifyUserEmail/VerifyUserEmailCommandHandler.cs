using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;
using Pollit.SeedWork;

namespace Pollit.Application.Users.VerifyUserEmail;

public class VerifyUserEmailCommandHandler : OperationHandlerBase<VerifyUserEmailCommand, IVerifyUserEmailPresenter>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public VerifyUserEmailCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    protected override async Task HandleAsync(AuthorizedOperation<VerifyUserEmailCommand> command, IVerifyUserEmailPresenter presenter)
    {
        var user = await _userRepository.GetAsync(new UserId(command.Value.UserId));
        if (user is null)
        {
            presenter.UserNotFound();
            return;
        }

        await user
            .VerifyEmail(new EmailVerificationToken(command.Value.EmailVerificationToken))
            .SwitchAsync(
                async success =>
                {
                    await _unitOfWork.SaveChangesAsync();
                    presenter.Success();
                },
                verificationTokenMismatchError => presenter.VerificationTokenMismatch());
    }
}