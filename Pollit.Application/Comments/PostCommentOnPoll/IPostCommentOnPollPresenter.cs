using Pollit.Domain.Comments;

namespace Pollit.Application.Comments.PostCommentOnPoll;

public interface IPostCommentOnPollPresenter : IPresenter
{
    void Success();
    void PollDoesNotExist(string error = ApplicationError.PollNotFound);
    void ParentCommentDoesNotExist(string error = ApplicationError.CommentNotFound);
    void ParentCommentIsDeleted(string error = ApplicationError.ParentCommentIsDeleted);
    void UserDoesNotExist(string error = ApplicationError.UserNotFound);
}