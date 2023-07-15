using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.ResetPasswordLinks;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Domain.Users._Ports;

public interface IUserRepository
{
    Task AddAsync(User user);
    void Update(User user);
    
    
    Task<bool> ExistsAsync(UserId userId);
    Task<bool> EmailExistsAsync(Email email);
    Task<bool> UserNameExistsAsync(UserName userName);
    
    Task<User?> FindByIdAsync(UserId userId);
    Task<User?> FindByEmailAsync(Email email);
    Task<User?> FindByUserNameAsync(UserName userName);
}