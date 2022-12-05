using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class QuestionEfConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();
        builder.Property(q => q.Header).IsRequired().HasMaxLength(500);
        builder.Property(q => q.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(q => q.ViewsCount).IsRequired().HasDefaultValue(0);


        builder.HasMany(q => q.AppUsersQuestionInteractions)
            .WithOne(aqi => aqi.QuestionIdNavigation)
            .HasForeignKey(aqi => aqi.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.Answers)
            .WithOne(a => a.ParentQuestionIdNavigation)
            .HasForeignKey(a => a.ParentQuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.TagsInQuestion)
            .WithOne(tiq => tiq.QuestionIdNavigation)
            .HasForeignKey(tiq => tiq.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        
    }
}