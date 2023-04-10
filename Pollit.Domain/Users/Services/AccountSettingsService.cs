using OneOf;
using OneOf.Types;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.Birthdates;
using Pollit.Domain.Users.Errors;
using Pollit.Domain.Users.UserNames;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SetPermanentUserNameResult : OneOfBase<Success, UserDoesNotExistError, UserNameIsAlreadyPermanentError, UserNameAlreadyExistsError> { }

[GenerateOneOf]
public partial class SetUserGenderResult : OneOfBase<Success, UserDoesNotExistError> { }

[GenerateOneOf]
public partial class SetUserBirthdayResult : OneOfBase<Success, UserDoesNotExistError, BirthdateIsInTheFutureError> { }

public class AccountSettingsService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;

    public AccountSettingsService(IUserRepository userRepository, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<SetPermanentUserNameResult> SetPermanentUserNameAsync(UserId userId, UserName userName)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
            return new UserDoesNotExistError();
    
        if (user.HasPermanentUserName)
            return new UserNameIsAlreadyPermanentError();
    
        if (await _userRepository.UserNameExistsAsync(userName))
            return new UserNameAlreadyExistsError();
    
        user.SetPermanentUserName(userName);
    
        return new Success();
    }

    public async Task<SetUserGenderResult> SetUserGender(Guid userId, EGender gender)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
            return new UserDoesNotExistError();

        user.SetGender(gender);
    
        return new Success();
    }

    public async Task<SetUserBirthdayResult> SetUserBirthdate(Guid userId, Birthdate? birthdate)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
            return new UserDoesNotExistError();

        if (birthdate is not null && _dateTimeProvider.UtcNow <= birthdate)
            return new BirthdateIsInTheFutureError();

        user.SetBirthdate(birthdate);
    
        return new Success();
    }
}