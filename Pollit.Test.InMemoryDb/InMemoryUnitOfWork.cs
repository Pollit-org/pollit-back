using Pollit.Domain._Ports;
using Pollit.SeedWork;

namespace Pollit.Test.InMemoryDb;

public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly Action _onSaveChanges;

    internal InMemoryUnitOfWork(Action onSaveChanges)
    {
        _onSaveChanges = onSaveChanges;
    }
    
    public void SaveChanges()
    {
        _onSaveChanges();
    }
    
    public Task SaveChangesAsync()
    {
        return _onSaveChanges.Async();
    }
}