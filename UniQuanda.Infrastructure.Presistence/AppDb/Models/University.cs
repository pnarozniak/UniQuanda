namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class University
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public virtual ICollection<AppUserInUniversity> AppUsersInUniversity { get; set; }
}