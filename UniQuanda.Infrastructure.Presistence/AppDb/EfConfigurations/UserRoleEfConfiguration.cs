using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class UserRoleEfConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.Id).ValueGeneratedOnAdd();

            builder.Property(ur => ur.ValidUnitl).IsRequired(false);

            builder.HasOne(ur => ur.AppUserIdNavigation)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.RoleIdNavigation)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var roles = new List<UserRole>
                {
                    new UserRole { Id = 1, AppUserId = 1, RoleId = 1},
                    new UserRole { Id = 2, AppUserId = 1, RoleId = 2}
                };

                builder.HasData(roles);
            }

        }
    }
}
