using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class PermissionUsageByUserEfConfiguration : IEntityTypeConfiguration<PermissionUsageByUser>
    {
        public void Configure(EntityTypeBuilder<PermissionUsageByUser> builder)
        {
            builder.HasKey(ul => ul.Id);
            builder.Property(ul => ul.Id).ValueGeneratedOnAdd();

            builder.Property(ul => ul.UsedTimes).IsRequired(true);

            builder.HasOne(ul => ul.PermissionIdNavigation)
                .WithMany(p => p.PermissionUsageByUsers)
                .HasForeignKey(ul => ul.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ul => ul.AppUserIdNavigation)
                .WithMany(u => u.UsedLimitsByUsers)
                .HasForeignKey(ul => ul.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
