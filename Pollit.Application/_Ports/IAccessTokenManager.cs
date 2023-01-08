using System.Security.Claims;
using Pollit.Domain.Users;

namespace Pollit.Application._Ports;

public interface IAccessTokenManager
{
    AccessToken Build(IEnumerable<Claim> claims);
    bool IsValid(AccessToken accessToken, out Dictionary<string, string> decodedToken);
    IEnumerable<Claim> Decrypt(AccessToken accessToken);
}