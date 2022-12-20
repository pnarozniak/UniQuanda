namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public int? LimitRefreshPeriod { get; set; }
        public int? AllowedUsages { get; set; }
        public virtual Permission PermissionIdNavigation { get; set; }
        public virtual Role RoleIdNavigation { get; set; }
    }
}
