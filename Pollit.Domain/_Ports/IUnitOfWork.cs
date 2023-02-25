namespace Pollit.Domain._Ports;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}