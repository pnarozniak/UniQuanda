namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUserAnswerInteraction
{
    public int Id { get; set; }
    public int AnswerId { get; set; }
    public int AppUserId { get; set; }
    public bool IsCreator { get; set; }
    public virtual Answer AnswerIdNavigation { get; set; }
    public virtual AppUser AppUserIdNavigation { get; set; }
}