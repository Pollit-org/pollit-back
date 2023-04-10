using Pollit.Domain.Poll;
using Pollit.Domain.Poll._Ports;

namespace Pollit.Test.InMemoryDb.Polls;

public class PollInMemoryRepository : BaseInMemoryRepository<Poll, PollId>, IPollRepository
{
    Task IPollRepository.AddAsync(Poll poll) => base.AddAsync(poll);

    Task<Poll?> IPollRepository.GetAsync(PollId pollId) => base.GetByIdAsync(pollId);
}