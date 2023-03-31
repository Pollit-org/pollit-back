using Pollit.Domain.Users;

namespace Pollit.Domain._Ports;

public interface IGoogleAuthenticator
{
    Task<GoogleProfile> AuthenticateAsync(string code);
}