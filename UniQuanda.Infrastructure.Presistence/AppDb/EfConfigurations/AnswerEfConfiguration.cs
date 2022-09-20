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

        builder.HasMany(a => a.AppUsersAnswerInteractions)
            .WithOne(uai => uai.AnswerIdNavigation)
            .HasForeignKey(uai => uai.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Comments)
            .WithOne(a => a.ParentAnswerIdNavigation)
            .HasForeignKey(a => a.ParentAnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.ParentAnswerIdNavigation)
            .WithMany(a => a.Comments);

        builder.HasOne(a => a.ParentQuestionIdNavigation)
            .WithMany(q => q.Answers);

        builder.Property(q => q.IsDeleted).HasDefaultValue(false).IsRequired();
    }
}