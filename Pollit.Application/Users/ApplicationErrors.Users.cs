namespace Pollit.Application;

public partial class ApplicationError
{
    private const string UsersErrorPrefix = $"{GlobalErrorPrefix}:USERS";

    public const string UserNotFound = $"{UsersErrorPrefix}:USER_NOT_FOUND";
    public const string UserNameIsAlreadyPermanent = $"{UsersErrorPrefix}:USER_NAME_IS_ALREADY_PERMANENT";
}