using System.Threading.Tasks;

namespace Pollit.Application._Ports;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}