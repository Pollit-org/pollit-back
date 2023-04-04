using Pollit.Domain.Users;

namespace Pollit.Domain._Ports;

public interface IGoogleAuthenticator
{
    Task<GoogleProfileDto> AuthenticateAsync(string code);
}