namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<PermissionUsageByUser> PermissionUsageByUsers { get; set; }
    }
}
