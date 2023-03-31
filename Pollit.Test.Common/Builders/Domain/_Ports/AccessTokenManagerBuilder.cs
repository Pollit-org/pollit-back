using Pollit.Infra.Jwt;

namespace Pollit.Test.Common.Builders.Domain._Ports;

public class AccessTokenManagerBuilder : IFluentBuilder<AccessTokenManager>
{
    private JwtConfig _config;
    
    public AccessTokenManagerBuilder()
    {
        WithDefaultConfig();
    }

    public AccessTokenManagerBuilder WithDefaultConfig()
        => WithConfig(BuildDefaultConfig());

    public AccessTokenManagerBuilder WithConfig(JwtConfig config)
    {
        _config = config;
        return this;
    }
    
    private JwtConfig BuildDefaultConfig() => new()
    {
        Audience = "test.pollit.me",
        SecretKey = "this-is-a-test-secret-key",
        ExpiryInMinutes = 60,
        Issuer = "test.pollit.me"
    };
    
    public AccessTokenManager Build()
    {
        return new AccessTokenManager(_config);
    }
}