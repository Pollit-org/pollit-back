using Pollit.Domain._Ports;
using Pollit.Domain.Polls._Ports;
using Pollit.Domain.Users._Ports;
using Pollit.SeedWork;

namespace Pollit.Application.Polls.CastVoteToPoll;

public class CastVoteToPollCommandHandler : OperationHandlerBase<CastVoteToPollCommand, ICastVoteToPollPresenter>
{
    private readonly IUserRepository _userRepository;
    private readonly IPollRepository _pollRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CastVoteToPollCommandHandler(IUserRepository userRepository, IPollRepository pollRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _pollRepository = pollRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<CastVoteToPollCommand> command, ICastVoteToPollPresenter presenter)
    {
        var user = await _userRepository.FindByIdAsync(command.Value.VoterId);
        if (user is null) 
        {
            presenter.VoterNotFound();
            return;
        }

        var poll = await _pollRepository.GetAsync(command.Value.PollId);
        if (poll is null) 
        {
            presenter.PollNotFound();
            return;
        }

        var result = poll.CastVote(user, command.Value.OptionId);
        
        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();
                presenter.Success();
            },
            optionDoesNotExistError => presenter.OptionNotFound(),
            userHasAlreadyVotedError => presenter.UserHasAlreadyVoted());
        
    }
}