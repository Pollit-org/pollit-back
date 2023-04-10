namespace Pollit.Domain.Poll._Ports;

public interface IPollRepository
{
    Task AddAsync(Poll poll);
    
    Task<Poll?> GetAsync(PollId userId);
}