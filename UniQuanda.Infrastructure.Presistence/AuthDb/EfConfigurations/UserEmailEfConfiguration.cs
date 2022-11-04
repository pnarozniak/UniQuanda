using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;

public class UserEmailEfConfiguration : IEntityTypeConfiguration<UserEmail>
{
    public void Configure(EntityTypeBuilder<UserEmail> builder)
    {
        builder.HasKey(ue => ue.Id);
        builder.Property(ue => ue.Id).ValueGeneratedOnAdd();

        builder.Property(ue => ue.Value).HasMaxLength(320).IsRequired();
        builder.HasIndex(ue => ue.Value).IsUnique();

        builder.Property(ue => ue.IsMain).IsRequired();

        builder
            .HasOne(ue => ue.IdUserActionToConfirmNavigation)
            .WithOne(ua => ua.IdUserEmailNavigation)
            .HasForeignKey<UserActionToConfirm>(ua => ua.IdUserEmail)
            .OnDelete(DeleteBehavior.Cascade);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var userEmails = new List<UserEmail>
            {
                new() { Id = 1, Value = "user@uniquanda.pl", IsMain = true, IdUser = 1 }
            };

            builder.HasData(userEmails);
        }
    }
}