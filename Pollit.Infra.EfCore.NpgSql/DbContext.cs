using Microsoft.EntityFrameworkCore;
using Pollit.Domain.Poll;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql;

public class PollitDbContext : DbContext
{
    public PollitDbContext(DbContextOptions<PollitDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Poll> Polls { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PollitDbContext).Assembly);
    }
} 