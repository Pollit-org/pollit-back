using Pollit.Domain.Poll;
using Pollit.Domain.Poll._Ports;

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

    public async Task<Poll?> GetAsync(PollId pollId)
    {
        return await _context.Polls.FindAsync(pollId);
    }
}