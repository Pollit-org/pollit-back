using System.Threading.Tasks;
using Pollit.Domain._Ports;

namespace Pollit.Infra.EfCore.NpgSql;

public class UnitOfWork : IUnitOfWork
{
    private readonly PollitDbContext _context;

    public UnitOfWork(PollitDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}