using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Question
{
    public int Id { get; set; }
    public string Header { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ViewsCount { get; set; }
    public int ContentId { get; set; }
    public virtual ICollection<TagInQuestion> TagsInQuestion { get; set; }
    public virtual ICollection<AppUserQuestionInteraction> AppUsersQuestionInteractions { get; set; }
    public virtual ICollection<Answer> Answers { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
    public virtual Content ContentIdNavigation { get; set; }
}