using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class UserPointsInTagEfConfiguration : IEntityTypeConfiguration<UserPointsInTag>
{
    public void Configure(EntityTypeBuilder<UserPointsInTag> builder)
    {
        builder.HasKey(tiq => tiq.Id);
        builder.Property(tiq => tiq.Id).ValueGeneratedOnAdd();

        builder.HasOne(ut => ut.TagIdNavigation)
            .WithMany(t => t.UsersPointsInTag)
            .HasForeignKey(ut => ut.TagId);

        builder.HasOne(ut => ut.AppUserIdNavigation)
            .WithMany(u => u.UserPointsInTags)
            .HasForeignKey(ut => ut.AppUserId);

        builder.HasIndex(ut => new { ut.AppUserId, ut.TagId }).IsUnique();

        builder.Property(ut => ut.Points).IsRequired();

    }
}