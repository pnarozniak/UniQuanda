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
                    Id=1,
                    RoleId = 1,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    Id = 2,
                    RoleId = 2,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                },
                new RolePermission()
                {
                    Id = 3,
                    RoleId = 3,
                    PermissionId = 1,
                },
                new RolePermission()
                {
                    Id = 4,
                    RoleId = 4,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 5
                },
                new RolePermission()
                {
                    Id = 5,
                    RoleId = 5,
                    PermissionId = 1,
                    LimitRefreshPeriod = 604800,
                    AllowedUsages = 3
                }
            );
            
        }
    }
}
