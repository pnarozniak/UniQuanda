namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? ValidUnitl { get; set; }
        public virtual AppUser AppUserIdNavigation { get; set; }
        public virtual Role RoleIdNavigation { get; set; }
    }
}
