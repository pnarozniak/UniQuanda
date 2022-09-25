namespace UniQuanda.Core.Domain.Entities.App;

public class AppUserEntity
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? SemanticScholarProfile { get; set; }
    public string? AboutText { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
}
