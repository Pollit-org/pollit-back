using System.Threading.Tasks;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
    void Update(User user);
    
    Task<bool> EmailExistsAsync(Email email);
    Task<bool> UserNameExistsAsync(UserName userName);
    
    Task<User?> FindByEmailAsync(Email email);
    Task<User?> FindByUserNameAsync(UserName userName);
    Task<User?> GetAsync(UserId userId);
}