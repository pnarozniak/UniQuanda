namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class UserPointsInTag
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int TagId { get; set; }
    public int Points { get; set; }
    public virtual Tag TagIdNavigation { get; set; }
    public virtual AppUser AppUserIdNavigation { get; set; }
}