using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class PermissionDataSeed : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission()
                {
                    Id = 1,
                    Name = "ask-question",
                },
                new Permission()
                {
                    Id = 2,
                    Name = "create-course",
                },
                new Permission()
                {
                    Id = 3,
                    Name = "solve-automatic-test",
                },
                new Permission()
                {
                    Id = 4,
                    Name = "solve-course",
                }
            );
        }
    }
}
