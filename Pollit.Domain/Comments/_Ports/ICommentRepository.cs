using Pollit.Domain.Polls;

namespace Pollit.Domain.Comments._Ports;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);

    Task<Comment?> GetAsync(CommentId commentId);
    Task<Comment?> GetAsync(PollId pollId, CommentId commentId);
}