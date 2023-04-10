using Pollit.Domain._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users.Services;
using Pollit.SeedWork;

namespace Pollit.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommandHandler : OperationHandlerBase<SignupWithCredentialsCommand, ISignupWithCredentialsPresenter>
{
    private readonly CredentialsAuthenticationService _credentialsAuthenticationService;
    private readonly IUnitOfWork _unitOfWork;

    public SignupWithCredentialsCommandHandler(CredentialsAuthenticationService credentialsAuthenticationService, IUnitOfWork unitOfWork)
    {
        _credentialsAuthenticationService = credentialsAuthenticationService;
        _unitOfWork = unitOfWork;
    }
    
    protected override async Task HandleAsync(AuthorizedOperation<SignupWithCredentialsCommand> authorizedCommand, ISignupWithCredentialsPresenter presenter)
    {
        var result = await _credentialsAuthenticationService.SignupWithCredentialsAsync(authorizedCommand.Value.Email, authorizedCommand.Value.UserName, authorizedCommand.Value.Password);

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success();
            },
            emailAlreadyExistsError => presenter.EMailAlreadyExists(),
            userNameAlreadyExistsError => presenter.UserNameAlreadyExists()
        );
    }
}