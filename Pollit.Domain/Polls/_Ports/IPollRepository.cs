namespace Pollit.Domain.Polls._Ports;

public interface IPollRepository
{
    Task AddAsync(Poll poll);
    void Update(Poll poll);
    
    Task<Poll?> GetAsync(PollId userId);
}