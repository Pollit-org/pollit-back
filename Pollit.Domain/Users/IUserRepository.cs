using Pollit.Domain.Shared.Email;

namespace Pollit.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
    void Update(User user);
    
    Task<bool> EmailExistsAsync(Email email);
    
    Task<User?> FindUserByEmail(Email email);
    Task<User?> GetAsync(UserId userId);
}