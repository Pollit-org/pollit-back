namespace Pollit.Application.Users.GetUserPrivateProfile;

public class GetUserPrivateProfileCommandHandler : QueryHandlerBase<GetUserPrivateProfileQuery, IGetUserPrivateProfilePresenter>
{
    protected override async Task HandleInternalAsync(GetUserPrivateProfileQuery query, IGetUserPrivateProfilePresenter presenter)
    {
        presenter.Success();
    }
}