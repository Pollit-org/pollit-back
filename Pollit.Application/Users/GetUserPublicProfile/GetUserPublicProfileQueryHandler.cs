using Pollit.Domain.Users._Ports;

namespace Pollit.Application.Users.GetUserPublicProfile;

public class GetUserPublicProfileQueryHandler : OperationHandlerBase<GetUserPublicProfileQuery, IGetUserPublicProfilePresenter>
{
    private readonly IUserRepository _userRepository;

    public GetUserPublicProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    protected override async Task HandleAsync(AuthorizedOperation<GetUserPublicProfileQuery> query, IGetUserPublicProfilePresenter presenter)
    {
        var user = await _userRepository.FindByIdAsync(query.Value.UserId);
        if (user is null)
        {
            presenter.UserNotFound();
            return;
        }
        
        presenter.Success(user.PublicProfile);
    }
}