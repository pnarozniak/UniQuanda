using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AppUserInUniversityEfConfiguration : IEntityTypeConfiguration<AppUserInUniversity>
{
    public void Configure(EntityTypeBuilder<AppUserInUniversity> builder)
    {
        builder.HasKey(uu => uu.Id);
        builder.Property(uu => uu.Id).ValueGeneratedOnAdd();

        builder.HasOne(uu => uu.UniversityIdNavigation)
            .WithMany(u => u.AppUsersInUniversity)
            .HasForeignKey(uu => uu.UniversityId);

        builder.HasOne(uu => uu.AppUserIdNavigation)
            .WithMany(u => u.AppUserInUniversities)
            .HasForeignKey(uu => uu.AppUserId);

        builder.HasIndex(uu => new { uu.UniversityId, uu.AppUserId }).IsUnique();
        builder.HasIndex(uu => new { uu.Order, uu.AppUserId }).IsUnique();

        builder.Property(uu => uu.Order).IsRequired();
    }
}