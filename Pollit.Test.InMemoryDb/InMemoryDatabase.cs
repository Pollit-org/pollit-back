using System.Reflection;
using Pollit.Domain.Polls._Ports;
using Pollit.Domain.Users;
using Pollit.Domain.Users._Ports;
using Pollit.SeedWork;
using Pollit.Test.InMemoryDb.Polls;
using Pollit.Test.InMemoryDb.Users;

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
    
    public UserInMemoryRepository GetUserRepository()
    {
        if (_repositories.TryGetValue(typeof(IUserRepository), out var repo))
            return (UserInMemoryRepository) repo;

        var inMemoryImplementation = new UserInMemoryRepository();
        _repositories.Add(typeof(IUserRepository), inMemoryImplementation);
        
        return inMemoryImplementation;
    }
    
    public PollInMemoryRepository GetPollRepository()
    {
        if (_repositories.TryGetValue(typeof(IPollRepository), out var repo))
            return (PollInMemoryRepository) repo;

        var inMemoryImplementation = new PollInMemoryRepository();
        _repositories.Add(typeof(IPollRepository), inMemoryImplementation);
        
        return inMemoryImplementation;
    }

    public InMemoryUnitOfWork GetUnitOfWork()
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