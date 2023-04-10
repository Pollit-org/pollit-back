namespace Pollit.Domain.Users;

public class GoogleProfileDto
{
    public string Email { get; set; }
    
    public string? Gender { get; set; }
    
    public int? BirthdayYear { get; set; }
    public int? BirthdayMonth { get; set; }
    public int? BirthdayDay { get; set; }
    
    public string? GivenName { get; set; }
    
    public string? FamilyName { get; set; }
    
    public string? Locale { get; set; }
    
    public string? Picture { get; set; }
    
    public bool? VerifiedEmail { get; set; }
}