using Pollit.Domain._Ports;
using Pollit.Domain.Polls;
using Pollit.Domain.Polls._Ports;
using Pollit.Domain.Polls.PollOptionTitles;
using Pollit.SeedWork;

namespace Pollit.Application.Polls.CreatePoll;

public class CreatePollCommandHandler : OperationHandlerBase<CreatePollCommand, ICreatePollPresenter>
{
    private readonly IPollRepository _pollRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePollCommandHandler(IPollRepository pollRepository, IUnitOfWork unitOfWork)
    {
        _pollRepository = pollRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<CreatePollCommand> command, ICreatePollPresenter presenter)
    {
        var optionTitles = command.Value.Options.Select(optTitle => new PollOptionTitle(optTitle));
        var tags = command.Value.Tags.Select(t => new PollTag(t));
        
        var pollCreationResult = Poll.NewPoll(command.Value.AuthorId, command.Value.Title, optionTitles, tags);

        await pollCreationResult.SwitchAsync(
            async poll =>
            {
                await _pollRepository.AddAsync(poll);
                await _unitOfWork.SaveChangesAsync();
                presenter.Success(poll.Id);
            },
            pollMustHaveAtLeastTwoOptionsError => presenter.PollMustHaveAtLeastTwoOptions()
        );

    }
}