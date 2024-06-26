﻿using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Test.InMemoryDb.Users;

public class UserInMemoryRepository : BaseInMemoryRepository<User, UserId>, IUserRepository
{
    Task IUserRepository.AddAsync(User user) => base.AddAsync(user);

    void IUserRepository.Update(User user) => base.Update(user);
    
    public Task<bool> ExistsAsync(UserId userId) => base.ExistsAsync(u => u.Id == userId);

    Task<bool> IUserRepository.EmailExistsAsync(Email email) => base.ExistsAsync(u => u.Email == email);

    Task<bool> IUserRepository.UserNameExistsAsync(UserName userName) => base.ExistsAsync(u => u.UserName == userName);

    Task<User?> IUserRepository.FindByEmailAsync(Email email) => base.FirstOrDefaultAsync(u => u.Email == email);

    Task<User?> IUserRepository.FindByUserNameAsync(UserName userName) => base.FirstOrDefaultAsync(u => u.UserName == userName);

    Task<User?> IUserRepository.FindByIdAsync(UserId userId) => base.GetByIdAsync(userId);
}