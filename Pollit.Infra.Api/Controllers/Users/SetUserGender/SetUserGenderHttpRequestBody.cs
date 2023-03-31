using System.Text.Json.Serialization;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Users.SetUserGender;

public class SetUserGenderHttpRequestBody
{
    public EGender? Gender { get; set; }
}