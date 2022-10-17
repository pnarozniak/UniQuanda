namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUserInUniversity
{
    public int Id { get; set; }
    public int UniversityId { get; set; }
    public int AppUserId { get; set; }
    public int Order { get; set; }
    public virtual AppUser AppUserIdNavigation { get; set; }
    public virtual University UniversityIdNavigation { get; set; }
}