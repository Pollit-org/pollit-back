using System.Threading.Tasks;
using Pollit.Domain.Users;

namespace Pollit.Application._Ports;

public interface IGoogleAuthenticator
{
    Task<GoogleProfile> Authenticate(string code);
}