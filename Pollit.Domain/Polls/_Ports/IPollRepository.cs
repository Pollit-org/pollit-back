namespace Pollit.Domain.Polls._Ports;

public interface IPollRepository
{
    Task AddAsync(Poll poll);

    Task<Poll?> GetAsync(PollId pollId);
    Task<bool> ExistsAsync(PollId pollId);
}