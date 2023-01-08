using Pollit.Application._Ports;
using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncryptor _passwordEncryptor;

    public SignupWithCredentialsCommandHandler(IUserRepository userRepository, IAccessTokenManager accessTokenManager, IPasswordEncryptor passwordEncryptor, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(SignupWithCredentialsCommand command, ISignupWithCredentialsPresenter presenter)
    {
        if (await _userRepository.EmailExistsAsync(command.Email))
        {
            presenter.EMailAlreadyTaken();
            return;
        }

        var encryptedPassword = _passwordEncryptor.Encrypt(command.Password);

        var user = User.NewUser(command.Email, encryptedPassword);

        await _userRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync();
        
       presenter.Success();
    }
}