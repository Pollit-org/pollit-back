using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Comments.PostCommentOnPoll;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Comments.PostCommentOnPoll;

[ApiController]
public class PostCommentOnPollController : OperationControllerBase<PostCommentOnPollCommand, IPostCommentOnPollPresenter, PostCommentOnPollPresenter, PostCommentOnPollCommandHandler>
{
    public PostCommentOnPollController(PostCommentOnPollCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [Authorize(Policy = CPolicies.PermanentUserNameAndEmailVerified)]
    [HttpPost("polls/{pollId:guid}/comments", Name = "PostCommentOnPoll")]
    public async Task<IActionResult?> PostCommentOnPollAsync([FromRoute] Guid pollId, [FromBody] PostCommentOnPollHttpRequestBody requestBody)
    {
        var command = new PostCommentOnPollCommand(pollId, requestBody.ParentCommentId, requestBody.CommentBody);

        var presenter = new PostCommentOnPollPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}

public class PostCommentOnPollPresenter : BasePresenter, IPostCommentOnPollPresenter
{
    public void Success() => OkNoContent();

    public void PollDoesNotExist(string error)
        => NotFound(error);

    public void ParentCommentDoesNotExist(string error)
        => NotFound(error);

    public void ParentCommentIsDeleted(string error)
        => Conflict(error);

    public void UserDoesNotExist(string error)
        => NotFound(error);
}