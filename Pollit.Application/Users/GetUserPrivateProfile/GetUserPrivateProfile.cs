using Pollit.Domain.Users;

namespace Pollit.Application.Users.GetUserPrivateProfile;

public class GetUserPrivateProfileCommandHandler : OperationHandlerBase<GetUserPrivateProfileQuery, IGetUserPrivateProfilePresenter>
{
    protected override async Task HandleAsync(AuthorizedOperation<GetUserPrivateProfileQuery> query, IGetUserPrivateProfilePresenter presenter)
    {
        presenter.Success();
    }
}