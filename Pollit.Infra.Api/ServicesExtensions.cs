using System.Reflection;

namespace Pollit.Infra.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddQueryAndCommandHandlers(this IServiceCollection services)
    {
        foreach (var handlerType in Assembly.Load("Pollit.Application")!.GetTypes().Where(t => t.Name.EndsWith("CommandHandler") || t.Name.EndsWith("QueryHandler"))) 
            services.AddTransient(handlerType, handlerType);

        return services;
    }
    
    public static IServiceCollection BindConfigurationSectionAsSingleton<TConfig>(this IServiceCollection services, IConfigurationSection configurationSection) where TConfig : new()
    {
        var instance = new TConfig();
        configurationSection.Bind(instance);
        return services.AddSingleton(typeof(TConfig), instance);
    }
}