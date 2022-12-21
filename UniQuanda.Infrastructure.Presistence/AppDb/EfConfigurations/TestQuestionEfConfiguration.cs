using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TestQuestionEfConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.HasKey(tq => tq.Id);
        builder.Property(tq => tq.Id).ValueGeneratedOnAdd();

        builder.HasOne(tq => tq.IdTestNavigation)
            .WithMany(t => t.TestQuestions)
            .HasForeignKey(tq => tq.IdTest)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tq => tq.IdQuestionNavigation)
            .WithMany(q => q.TestsQuestions)
            .HasForeignKey(tq => tq.IdQuestion)
            .OnDelete(DeleteBehavior.Cascade);
    }
}