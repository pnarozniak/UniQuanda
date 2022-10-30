using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TagEfConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.HasMany(t => t.TagInQuestions)
            .WithOne(tq => tq.TagIdNavigation)
            .HasForeignKey(tq => tq.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.UsersPointsInTag)
            .WithOne(ut => ut.TagIdNavigation)
            .HasForeignKey(ut => ut.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.ChildTags)
            .WithOne(ct => ct.ParentTagIdNavigation)
            .HasForeignKey(ct => ct.ParentTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
    }
}