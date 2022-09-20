using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TagInQuestionEfConfiguration : IEntityTypeConfiguration<TagInQuestion>
{
    public void Configure(EntityTypeBuilder<TagInQuestion> builder)
    {
        builder.HasKey(tiq => tiq.Id);
        builder.Property(tiq => tiq.Id).ValueGeneratedOnAdd();

        builder.HasOne(tiq => tiq.TagIdNavigation)
            .WithMany(t => t.TagInQuestions)
            .HasForeignKey(tiq => tiq.TagId);

        builder.HasOne(tiq => tiq.QuestionIdNavigation)
            .WithMany(q => q.TagsInQuestion)
            .HasForeignKey(tiq => tiq.QuestionId);

        builder.HasIndex(tiq => new { tiq.QuestionId, tiq.TagId }).IsUnique();
        builder.HasIndex(tiq => new { tiq.QuestionId, tiq.Order }).IsUnique();

        builder.Property(tiq => tiq.Order).IsRequired();
    }
}