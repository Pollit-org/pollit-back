using OneOf;
using OneOf.Types;
using Pollit.Domain.Comments._Ports;
using Pollit.Domain.Comments.Errors;
using Pollit.Domain.Polls;
using Pollit.Domain.Polls._Ports;
using Pollit.Domain.Polls.Errors;
using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;
using Pollit.Domain.Users.Errors;

namespace Pollit.Domain.Comments;

[GenerateOneOf]
public partial class PostCommentOnPollResult : OneOfBase<Success<CommentId>, PollDoesNotExistError, ParentCommentDoesNotExistError, ParentCommentIsDeletedError, UserDoesNotExistError> { }

public class PollCommentingService
{
    private readonly IPollRepository _pollRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public PollCommentingService(IPollRepository pollRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _pollRepository = pollRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<PostCommentOnPollResult> PostCommentOnPoll(UserId commentAuthorId, PollId pollId, CommentId? parentCommentId, CommentBody body)
    {
        if (! await _userRepository.ExistsAsync(commentAuthorId))
            return new UserDoesNotExistError();

        var poll = await _pollRepository.GetAsync(pollId);
        if (poll is null)
            return new PollDoesNotExistError();

        if (parentCommentId is not null)
        {
            var parentComment = await _commentRepository.GetAsync(pollId, parentCommentId);
            if (parentComment is null)
                return new ParentCommentDoesNotExistError();
            if (parentComment.IsDeleted)
                return new ParentCommentIsDeletedError();
        }

        var comment = Comment.NewComment(pollId, parentCommentId, commentAuthorId, body);
        
        await _commentRepository.AddAsync(comment);

        return new Success<CommentId>(comment.Id);
    }
}