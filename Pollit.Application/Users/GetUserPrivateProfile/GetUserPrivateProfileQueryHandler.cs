using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;

namespace Pollit.Application.Users.GetUserPrivateProfile;

public class GetUserPrivateProfileQueryHandler : OperationHandlerBase<GetUserPrivateProfileQuery, IGetUserPrivateProfilePresenter>
{
    private readonly IUserRepository _userRepository;

    public GetUserPrivateProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    protected override async Task HandleAsync(AuthorizedOperation<GetUserPrivateProfileQuery> query, IGetUserPrivateProfilePresenter presenter)
    {
        var user = await _userRepository.FindByIdAsync(query.Value.UserId);
        if (user is null)
        {
            presenter.UserNotFound();
            return;
        }
        
        presenter.Success(user.PrivateProfile);
    }
    
}