using Microsoft.EntityFrameworkCore;
using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.Application.Polls.GetPollFeed;
using Pollit.Domain.Comments;
using Pollit.Domain.Polls;
using Pollit.Domain.Users;
using Pollit.Infra.EfCore.NpgSql.Projections.Comments;

namespace Pollit.Infra.EfCore.NpgSql;

public class PollitDbContext : DbContext
{
    public PollitDbContext(DbContextOptions<PollitDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Poll> Polls { get; set; } = null!;
    
    public DbSet<GetPollFeedQueryResultItem> PollFeedItems { get; set; } = null!;
    
    public DbSet<Comment> Comments { get; set; } = null!;
    
    public DbSet<GetCommentsOfAPollRawResultItem> CommentOfAPollRawItems { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PollitDbContext).Assembly);
    }
} 