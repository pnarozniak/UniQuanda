namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class PermissionUsageByUser
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int UsedTimes { get; set; }
        public int AppUserId { get; set; }
        public virtual Permission PermissionIdNavigation { get; set; }
        public virtual AppUser AppUserIdNavigation { get; set; }
    }
}
