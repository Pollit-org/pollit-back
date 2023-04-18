using Pollit.Domain.Users;

namespace Pollit.Domain._Ports;

public interface IGoogleAuthenticator
{
    Task<GoogleProfileDto> AuthenticateWithAuthCodeAsync(string code);
    
    Task<GoogleProfileDto> AuthenticateWithAccessTokenAsync(string accessToken);
}