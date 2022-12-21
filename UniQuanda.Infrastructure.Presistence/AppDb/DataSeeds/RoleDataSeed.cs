using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class RoleDataSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role()
                {
                    Id = 1,
                    Name = AppRole.Admin,
                },
                new Role()
                {
                    Id = 2,
                    Name = AppRole.User,
                },
                new Role()
                {
                    Id = 3,
                    Name = AppRole.Premium,
                },
                new Role()
                {
                    Id = 4,
                    Name = AppRole.EduUser,
                },
                new Role()
                {
                    Id = 5,
                    Name = AppRole.TitledUser,
                });
        }
    }
}
