namespace Pollit.Infra.Api.Controllers.Users.SetUserBirthdate;

public class SetUserBirthdateHttpRequestBody
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
}