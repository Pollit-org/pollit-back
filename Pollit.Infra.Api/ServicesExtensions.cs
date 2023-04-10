using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pollit.Application;
using Pollit.Domain.Users;
using Pollit.Infra.Jwt;

namespace Pollit.Infra.Api;

public static class ServicesExtensions
{
    public static IServiceCollection AddQueryAndCommandHandlers(this IServiceCollection services)
    {
        foreach (var handlerType in Assembly.Load("Pollit.Application")!.GetTypes().Where(t => t.IsAssignableTo(typeof(OperationHandlerBase)) && !t.IsAbstract))
            services.AddTransient(handlerType, handlerType);

        return services;
    }
    
    public static IServiceCollection BindConfigurationSectionAsSingleton<TConfig>(this IServiceCollection services, IConfigurationSection configurationSection) where TConfig : new()
    {
        return services.BindConfigurationSectionAsSingleton<TConfig>(configurationSection, out _);
    }
    
    public static IServiceCollection BindConfigurationSectionAsSingleton<TConfig>(this IServiceCollection services, IConfigurationSection configurationSection, out TConfig configInstance) where TConfig : new()
    {
        configInstance = new TConfig();
        configurationSection.Bind(configInstance);
        return services.AddSingleton(typeof(TConfig), configInstance);
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
        return services;
    }
    
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            options.AddPolicy(CPolicies.PermanentUserName, policy => policy.RequireClaim(CClaimTypes.HasTemporaryUserName, false.ToString()));
            options.AddPolicy(CPolicies.EmailVerified, policy => policy.RequireClaim(CClaimTypes.IsEmailVerified, true.ToString()));
            options.AddPolicy(CPolicies.Authenticated, policy => policy.RequireClaim(CClaimTypes.UserId));
            options.AddPolicy(CPolicies.PermanentUserNameAndEmailVerified, policy => policy.Combine(options.GetPolicy(CPolicies.PermanentUserName)!).Combine(options.GetPolicy(CPolicies.EmailVerified)!));
        });
    }
}