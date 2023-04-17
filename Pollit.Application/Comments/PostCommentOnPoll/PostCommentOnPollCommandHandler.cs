using Pollit.Domain._Ports;
using Pollit.Domain.Comments;
using Pollit.Domain.Polls;
using Pollit.SeedWork;

namespace Pollit.Application.Comments.PostCommentOnPoll;

public class PostCommentOnPollCommandHandler : OperationHandlerBase<PostCommentOnPollCommand, IPostCommentOnPollPresenter>
{
    private readonly PollCommentingService _pollCommentingService;
    private readonly IUnitOfWork _unitOfWork;

    public PostCommentOnPollCommandHandler(PollCommentingService pollCommentingService, IUnitOfWork unitOfWork)
    {
        _pollCommentingService = pollCommentingService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleAsync(AuthorizedOperation<PostCommentOnPollCommand> command, IPostCommentOnPollPresenter presenter)
    {
        var result = await _pollCommentingService.PostCommentOnPoll(command.AuthorizedFor, command.Value.PollId, command.Value.ParentCommentId, new CommentBody(command.Value.Body));

        await result.SwitchAsync(
            async success =>
            {
                await _unitOfWork.SaveChangesAsync();

                presenter.Success();
            },
            pollDoesNotExistError => presenter.PollDoesNotExist(),
            parentCommentDoesNotExistError => presenter.ParentCommentDoesNotExist(),
            parentCommentIsDeletedError => presenter.ParentCommentIsDeleted(),
            userDoesNotExistError => presenter.UserDoesNotExist()
            );
    }
}