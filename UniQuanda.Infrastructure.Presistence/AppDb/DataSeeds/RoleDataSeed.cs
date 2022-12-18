using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Core.Domain.Enums;
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
                    Name = RoleNameEnum.Admin,
                },
                new Role()
                {
                    Id = 2,
                    Name = RoleNameEnum.User,
                },
                new Role()
                {
                    Id =3,
                    Name = RoleNameEnum.Premium,
                },
                new Role()
                {
                    Id = 4,
                    Name = RoleNameEnum.EduUser,
                },
                new Role()
                {
                    Id = 5,
                    Name = RoleNameEnum.TitledUser,
                });
        }
    }
}
