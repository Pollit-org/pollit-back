using Microsoft.EntityFrameworkCore;
using Pollit.Infra.EfCore.NpgSql;
using Pollit.SeedWork.Eventing;

namespace Pollit.Infra.Api;

public static class WebApplicationExtensions
{
    public static void RegisterDomainEventHandler<TEvent, TConsumer>(this WebApplication app)
        where TEvent : IDomainEvent 
        where TConsumer : IDomainEventConsumer<TEvent>
    {
        app.Services.GetService<DomainEventBus>()!.RegisterHandler<TEvent>(@event =>
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<TConsumer>().ConsumeAsync(@event).GetAwaiter().GetResult();
        });
    }
    
    public static void ApplyDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope
            .ServiceProvider
            .GetRequiredService<PollitDbContext>()
            .Database
            .Migrate();
    }
}