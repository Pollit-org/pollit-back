using Microsoft.EntityFrameworkCore;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly PollitDbContext _context;
    
    public UserRepository(PollitDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public Task<bool> EmailExistsAsync(Email email)
    {
        return _context.Users.AnyAsync(u => (string) (object) u.Email == email.Value);
    }

    public Task<bool> UserNameExistsAsync(UserName userName)
    {
        return _context.Users.AnyAsync(u => (string) (object) u.UserName == userName.Value);
    }

    public Task<User?> FindUserByEmail(Email email)
    {
        return _context.Users.FirstOrDefaultAsync(u => (string) (object) u.Email == email.Value);
    }

    public async Task<User?> GetAsync(UserId userId)
    {
        return await _context.Users.FindAsync(userId.Value);
    }
}