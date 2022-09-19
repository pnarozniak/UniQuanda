namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUserQuestionInteraction
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int AppUserId { get; set; }
    public bool IsCreator { get; set; }
    public virtual AppUser AppUserIdNavigation { get; set; }
    public virtual Question QuestionIdNavigation { get; set; }
}