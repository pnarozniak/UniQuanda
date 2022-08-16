using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;

public class TempUserEfConfiguration : IEntityTypeConfiguration<TempUser>
{
    public void Configure(EntityTypeBuilder<TempUser> builder)
    {
        builder.HasKey(tu => tu.IdUser);

        builder.Property(tu => tu.EmailConfirmationCode).HasMaxLength(6).IsRequired();

        builder.Property(tu => tu.ExistsUntil).IsRequired();

        builder.Property(tu => tu.FirstName).HasMaxLength(35).IsRequired(false);

        builder.Property(tu => tu.LastName).HasMaxLength(51).IsRequired(false);

        builder.Property(tu => tu.Birthdate).IsRequired(false);

        builder.Property(tu => tu.PhoneNumber).HasMaxLength(22).IsRequired(false);

        builder.Property(tu => tu.City).HasMaxLength(57).IsRequired(false);
    }
}