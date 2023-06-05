using Microsoft.EntityFrameworkCore;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.UserNames;

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

    public Task<bool> ExistsAsync(UserId userId)
    {
        return _context.Users.AnyAsync(u => u.Id == userId);
    }

    public Task<bool> EmailExistsAsync(Email email)
    {
        return _context.Users.AnyAsync(u => u.Email == email);
    }

    public Task<bool> UserNameExistsAsync(UserName userName)
    {
        return _context.Users.AnyAsync(u => u.UserName == userName);
    }

    public Task<User?> FindByEmailAsync(Email email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> FindByUserNameAsync(UserName userName)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<User?> GetAsync(UserId userId)
    {
        return await _context.Users.FindAsync(userId);
    }
}