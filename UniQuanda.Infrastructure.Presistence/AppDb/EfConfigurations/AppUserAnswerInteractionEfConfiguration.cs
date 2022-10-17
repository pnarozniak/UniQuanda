using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AppUserAnswerInteractionEfConfiguration : IEntityTypeConfiguration<AppUserAnswerInteraction>
{
    public void Configure(EntityTypeBuilder<AppUserAnswerInteraction> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id).ValueGeneratedOnAdd();

        builder.Property(ua => ua.IsCreator).IsRequired();

        builder.HasIndex(ua => new { ua.AppUserId, ua.AnswerId }).IsUnique();
    }
}