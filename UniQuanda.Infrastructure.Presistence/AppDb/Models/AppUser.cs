namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUser
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
    public virtual ICollection<AppUserInUniversity> AppUserInUniversities { get; set; }
    public virtual ICollection<UserPointsInTag> UserPointsInTags { get; set; }
    public virtual ICollection<AppUserQuestionInteraction> AppUserQuestionsInteractions { get; set; }
    public virtual ICollection<AppUserAnswerInteraction> AppUserAnswersInteractions { get; set; }
    public virtual ICollection<AppUserTitle> AppUserTitles { get; set; }
}