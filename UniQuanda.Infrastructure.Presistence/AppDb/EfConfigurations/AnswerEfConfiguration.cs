using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AnswerEfConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.LikeCount).IsRequired().HasDefaultValue(0);
        builder.Property(a => a.IsCorrect).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.HasBeenModified).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.CreatedAt).IsRequired();

        builder.HasMany(a => a.AppUsersAnswerInteractions)
            .WithOne(uai => uai.AnswerIdNavigation)
            .HasForeignKey(uai => uai.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Comments)
            .WithOne(a => a.ParentAnswerIdNavigation)
            .HasForeignKey(a => a.ParentAnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(q => q.IsDeleted).HasDefaultValue(false).IsRequired();
    }
}