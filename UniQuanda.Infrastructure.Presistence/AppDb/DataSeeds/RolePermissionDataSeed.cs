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
                    Id = 1,
                    RoleId = 1,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    Id = 2,
                    RoleId = 1,
                    PermissionId = 3,
                },
                new RolePermission()
                {
                    Id = 3,
                    RoleId = 2,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                },
                new RolePermission()
                {
                    Id = 4,
                    RoleId = 2,
                    PermissionId = 3,
                    LimitRefreshPeriod = 86400,
                    AllowedUsages = 1
                },
                new RolePermission()
                {
                    Id = 5,
                    RoleId = 3,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    Id = 6,
                    RoleId = 3,
                    PermissionId = 3,
                },
                new RolePermission()
                {
                    Id = 7,
                    RoleId = 4,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 5
                },
                new RolePermission()
                {
                    Id = 8,
                    RoleId = 4,
                    PermissionId = 3
                },
                new RolePermission()
                {
                    Id = 9,
                    RoleId = 5,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                },
                new RolePermission()
                {
                    Id = 10,
                    RoleId = 5,
                    PermissionId = 3,
                    LimitRefreshPeriod = 86400,
                    AllowedUsages = 1
                }
            );

        }
    }
}
