namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AppUserTitle
{
    public int Id { get; set; }
    public int AcademicTitleId { get; set; }
    public int AppUserId { get; set; }
    public int Order { get; set; }
    public virtual AcademicTitle AcademicTitleIdNavigation { get; set; }
    public virtual AppUser AppUserIdNavigation { get; set; }
}