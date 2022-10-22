namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.GetAppUserProfileSettings;

public class GetAppUserProfileSettingsResponseDTO
{
    public string NickName { get; set; }
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