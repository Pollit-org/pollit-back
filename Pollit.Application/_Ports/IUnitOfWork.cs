namespace Pollit.Application._Ports;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}