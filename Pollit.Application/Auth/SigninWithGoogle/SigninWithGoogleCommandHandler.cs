using Microsoft.Extensions.Logging;
using Pollit.Application._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithGoogle;

public class SigninWithGoogleCommandHandler
{
    private readonly IAccessTokenManager _accessTokenManager;
    private readonly IGoogleAuthenticator _googleAuthenticator;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SigninWithGoogleCommandHandler> _logger;

    public SigninWithGoogleCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IGoogleAuthenticator googleAuthenticator, ILogger<SigninWithGoogleCommandHandler> logger, IAccessTokenManager accessTokenManager)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _googleAuthenticator = googleAuthenticator;
        _logger = logger;
        _accessTokenManager = accessTokenManager;
    }
    
    public async Task HandleAsync(SigninWithGoogleCommand command, ISigninWithGooglePresenter presenter)
    {
        GoogleProfile googleProfile;
        try
        {
            googleProfile = await _googleAuthenticator.Authenticate(command.GoogleAuthenticationCode);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Google authentication failed");
            
            presenter.GoogleAuthenticationFailed();
            return;
        }

        var email = new Email(googleProfile.Email);
        
        var user = await _userRepository.FindByEmailAsync(email);
        
        if (user is null)
        {
            user = User.NewUser(email);

            await _userRepository.AddAsync(user);
        }

        user.OnLoginWithGoogle(googleProfile);
        
        var accessToken = _accessTokenManager.Build(user.GetClaims());
        var refreshToken = RefreshToken.Generate();
        
        user.AddRefreshToken(refreshToken);
        
        await _unitOfWork.SaveChangesAsync();
        
        presenter.Success(new SigninResult(accessToken, refreshToken));
    }
}