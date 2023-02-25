﻿using OneOf;
using OneOf.Types;
using Pollit.Domain.Users.Errors;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Domain.Users.Services;

[GenerateOneOf]
public partial class SetPermanentUserNameResult : OneOfBase<Success, UserDoesNotExistError, UserNameIsAlreadyPermanentError, UserNameAlreadyExistsError> { }

public class AccountSettingsService
{
    private readonly IUserRepository _userRepository;

    public AccountSettingsService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
}