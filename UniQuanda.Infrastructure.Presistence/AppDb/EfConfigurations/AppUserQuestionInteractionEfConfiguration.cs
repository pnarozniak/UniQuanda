using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AppUserQuestionInteractionEfConfiguration : IEntityTypeConfiguration<AppUserQuestionInteraction>
{
    public void Configure(EntityTypeBuilder<AppUserQuestionInteraction> builder)
    {
        builder.HasKey(uq => uq.Id);
        builder.Property(uq => uq.Id).ValueGeneratedOnAdd();

        builder.Property(uq => uq.IsCreator).IsRequired();

        builder.HasIndex(uq => new { uq.AppUserId, uq.QuestionId }).IsUnique();
    }
}