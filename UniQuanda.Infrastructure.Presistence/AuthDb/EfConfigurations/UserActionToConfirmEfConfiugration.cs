using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;

public class UserActionToConfirmEfConfiguration : IEntityTypeConfiguration<UserActionToConfirm>
{
    public void Configure(EntityTypeBuilder<UserActionToConfirm> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id).ValueGeneratedOnAdd();

        builder.Property(ua => ua.ConfirmationToken).IsRequired();

        builder.Property(ua => ua.ExistsUntil).IsRequired();

        builder.Property(ua => ua.ActionType).IsRequired();

        builder.Property(ua => ua.IdUserEmail).IsRequired(false);

        builder.HasOne(ua => ua.IdUserNavigation)
            .WithMany(u => u.ActionsToConfirm)
            .HasForeignKey(ua => ua.IdUser)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ua => new { ua.ActionType, ua.IdUser }).IsUnique();
    }
}