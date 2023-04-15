using Microsoft.EntityFrameworkCore;
using Pollit.Domain.Polls;
using Pollit.Domain.Polls._Ports;

namespace Pollit.Infra.EfCore.NpgSql.Repositories.Polls;

public class PollRepository : IPollRepository
{
    private readonly PollitDbContext _context;
    
    public PollRepository(PollitDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Poll poll)
    {
        await _context.Polls.AddAsync(poll);
    }

    public void Update(Poll poll)
    {
        _context.Update(poll);
    }

    public Task<Poll?> GetAsync(PollId pollId)
    {
        return _context.Polls
            .Include(p => p.Options)
            .ThenInclude(o => o.Votes)
            .FirstOrDefaultAsync(p => p.Id == pollId);
    }
}