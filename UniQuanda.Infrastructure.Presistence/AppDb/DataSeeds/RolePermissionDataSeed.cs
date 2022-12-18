using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class RolePermissionDataSeed : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasData(
                new RolePermission()
                {
                    RoleId = 1,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    RoleId = 1,
                    PermissionId = 3,
                },
                new RolePermission()
                {
                    RoleId = 2,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                },
                new RolePermission()
                {
                    RoleId = 2,
                    PermissionId = 3,
                    LimitRefreshPeriod = 86400,
                    AllowedUsages = 1
                },
                new RolePermission()
                {
                    RoleId = 3,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    RoleId = 3,
                    PermissionId = 3,
                },
                new RolePermission()
                {
                    RoleId = 4,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 5
                },
                new RolePermission()
                {
                    RoleId = 4,
                    PermissionId = 3
                },
                new RolePermission()
                {
                    RoleId = 5,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                },
                new RolePermission()
                {
                    RoleId = 5,
                    PermissionId = 3,
                    LimitRefreshPeriod = 86400,
                    AllowedUsages = 1
                }
            );
            
        }
    }
}
