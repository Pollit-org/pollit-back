using Microsoft.EntityFrameworkCore;
using Pollit.Domain._Ports;
using Pollit.SeedWork;
using Pollit.SeedWork.Eventing;

namespace Pollit.Infra.EfCore.NpgSql;

public class UnitOfWork : IUnitOfWork
{
    private readonly PollitDbContext _context;
    private readonly DomainEventBus _domainEventBus;

    public UnitOfWork(PollitDbContext context, DomainEventBus domainEventBus)
    {
        _context = context;
        _domainEventBus = domainEventBus;
    }

    public async Task SaveChangesAsync()
    {
        var savedEntities = _context.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Modified or EntityState.Deleted or EntityState.Added)
            .Select(x => x.Entity)
            .Where(x => x.GetType().IsAssignableTo(typeof(IEntity)))
            .ToArray();
        
        await _context.SaveChangesAsync();
        
        foreach (var savedEntity in savedEntities) 
            FlushEvents((IEntity) savedEntity);
    }

    private void FlushEvents(IEntity entity)
    {
        foreach (var @event in entity.DomainEvents) 
            _domainEventBus.Emit(@event);
        
        entity.ClearDomainEvents();
    }
}