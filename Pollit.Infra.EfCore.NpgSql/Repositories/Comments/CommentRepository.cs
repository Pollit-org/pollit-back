using Microsoft.EntityFrameworkCore;
using Pollit.Domain.Comments;
using Pollit.Domain.Comments._Ports;
using Pollit.Domain.Polls;

namespace Pollit.Infra.EfCore.NpgSql.Repositories.Comments;

public class CommentRepository : ICommentRepository
{
    private readonly PollitDbContext _context;
    
    public CommentRepository(PollitDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public Task<Comment?> GetAsync(CommentId commentId)
    {
        return Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public Task<Comment?> GetAsync(PollId pollId, CommentId commentId)
    {
        return Comments.FirstOrDefaultAsync(c => c.PollId == pollId && c.Id == commentId);
    }

    private IQueryable<Comment> Comments => _context.Comments.Include(c => c.Votes);
}