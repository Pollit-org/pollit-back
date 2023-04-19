using System.Security.Claims;
using Pollit.Domain.Users;

namespace Pollit.Domain._Ports;

public interface IAccessTokenManager
{
    AccessToken Build(IEnumerable<Claim> claims);
    bool IsValid(AccessToken accessToken, bool validateLifetime, out Dictionary<string, string> decodedToken);
    IEnumerable<Claim> Decrypt(AccessToken accessToken);
}