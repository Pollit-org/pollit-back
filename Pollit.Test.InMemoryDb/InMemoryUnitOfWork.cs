using Pollit.Application._Ports;
using Pollit.SeedWork;

namespace Pollit.Test.InMemoryDb;

internal class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly Action _onSaveChanges;

    public InMemoryUnitOfWork(Action onSaveChanges)
    {
        _onSaveChanges = onSaveChanges;
    }
    
    public Task SaveChangesAsync()
    {
        return _onSaveChanges.Async();
    }
}