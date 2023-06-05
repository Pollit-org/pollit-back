using System.ComponentModel.DataAnnotations;

namespace Pollit.Infra.Api.Controllers.Users.VerifyUserEmail;

public class VerifyUserEmailHttpRequestQueryParams
{
    [Required]
    public Guid EmailVerificationToken { get; set; }
}