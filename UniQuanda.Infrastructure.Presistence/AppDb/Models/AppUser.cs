using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUser
{
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Contact { get; set; }
    public string? City { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
    public string? SemanticScholarProfile { get; set; }
    public string? AboutText { get; set; }
    public virtual ICollection<AppUserInUniversity> AppUserInUniversities { get; set; }
    public virtual ICollection<UserPointsInTag> UserPointsInTags { get; set; }
    public virtual ICollection<AppUserQuestionInteraction> AppUserQuestionsInteractions { get; set; }
    public virtual ICollection<AppUserAnswerInteraction> AppUserAnswersInteractions { get; set; }
    public virtual ICollection<AppUserTitle> AppUserTitles { get; set; }
    public virtual ICollection<Report> CreatedReports { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
    public virtual GlobalRanking GlobalRankingNavigation { get; set; }
}