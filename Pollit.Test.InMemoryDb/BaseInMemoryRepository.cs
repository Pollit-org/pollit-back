using Pollit.SeedWork;

namespace Pollit.Test.InMemoryDb;

public abstract class BaseInMemoryRepository
{
    internal abstract void SaveChanges();
}

public abstract class BaseInMemoryRepository<TEntity, TEntityId> : BaseInMemoryRepository where TEntity : EntityBase<TEntityId>
{
    protected HashSet<TEntity> _entities = new();
    protected HashSet<TEntity> _entitiesToAdd = new();
    protected HashSet<TEntity> _entitiesToUpdate = new();
    protected HashSet<TEntity> _entitiesToDelete = new();
    
    public TEntity? GetById(TEntityId id)
    {
        return _entities.FirstOrDefault(u => u.Id.Equals(id));
    }

    public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
    {
        return _entities.FirstOrDefault(predicate);
    }
    
    public bool Exists(Func<TEntity, bool> predicate)
    {
        return FirstOrDefault(predicate) is not null;
    }

    public void Add(TEntity entity)
    {
        _entitiesToAdd.Add(entity);
        _entitiesToDelete.Remove(entity);
    }
    
    public void Update(TEntity entity)
    {
        _entitiesToUpdate.Add(entity);
    }
    
    public void Remove(TEntity entity)
    {
        _entitiesToDelete.Add(entity);
        _entitiesToAdd.Remove(entity);
        _entitiesToUpdate.Remove(entity);
    }

    public Task<TEntity?> GetByIdAsync(TEntityId id) => Asyncator.Async(GetById, id);
    public Task<TEntity?> FirstOrDefaultAsync(Func<TEntity, bool> predicate) => Asyncator.Async(FirstOrDefault, predicate);
    public Task AddAsync(TEntity entity) => Asyncator.Async(Add, entity);
    public Task<bool> ExistsAsync(Func<TEntity, bool> predicate) => Asyncator.Async(Exists, predicate);

    internal override void SaveChanges()
    {
        _entities.RemoveWhere(e => _entitiesToDelete.Contains(e));
        _entities.RemoveWhere(e => _entitiesToUpdate.Contains(e));
        foreach (var entityToUpdate in _entitiesToUpdate.Concat(_entitiesToAdd))
        {
            _entities.Add(entityToUpdate);
        }
    }
}