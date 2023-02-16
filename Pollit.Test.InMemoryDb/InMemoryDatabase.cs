using System.Reflection;
using Pollit.Application._Ports;
using Pollit.SeedWork;

namespace Pollit.Test.InMemoryDb;

public class InMemoryDatabase
{
    private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public TRepository GetRepository<TRepository>()
    {
        if (_repositories.TryGetValue(typeof(TRepository), out var repo))
            return (TRepository) repo;
        
        var inMemoryImplementationType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsAssignableTo(typeof(TRepository)));
        var inMemoryImplementation = (TRepository) Activator.CreateInstance(inMemoryImplementationType);
        _repositories.Add(typeof(TRepository), inMemoryImplementation);
        
        return inMemoryImplementation;
    }

    public IUnitOfWork GetUnitOfWork()
    {
        return new InMemoryUnitOfWork(OnSaveChanges);
    }

    private void OnSaveChanges()
    {
        foreach (var repository in this._repositories.Values)
        {
            ((BaseInMemoryRepository) repository).SaveChanges();
        }
    }
}