using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Answer
{
    public int Id { get; set; }
    public int ParentQuestionId { get; set; }
    public int? ParentAnswerId { get; set; }
    public int ContentId { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Question ParentQuestionIdNavigation { get; set; }
    public virtual Answer? ParentAnswerIdNavigation { get; set; }
    public virtual ICollection<Answer> Comments { get; set; }
    public virtual ICollection<AppUserAnswerInteraction> AppUsersAnswerInteractions { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
    public virtual Content ContentIdNavigation { get; set; }
}