using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations
{
    public class TempUserEfConfiguration : IEntityTypeConfiguration<TempUser>
    {
        public void Configure(EntityTypeBuilder<TempUser> builder)
        {
            builder.HasKey(tu => tu.IdUser);

            builder.Property(tu => tu.Email).HasMaxLength(320).IsRequired();
            builder.HasIndex(tu => tu.Email).IsUnique();

            builder.Property(tu => tu.EmailConfirmationCode).HasMaxLength(6).IsRequired();

            builder.Property(tu => tu.ExistsTo).IsRequired();
            
            builder.Property(tu => tu.FirstName).HasMaxLength(30).IsRequired(false);

            builder.Property(tu => tu.LastName).HasMaxLength(30).IsRequired(false);

            builder.Property(tu => tu.Birthdate).IsRequired(false);

            builder.Property(tu => tu.PhoneNumber).HasMaxLength(9).IsRequired(false);
            
            builder.Property(tu => tu.City).HasMaxLength(30).IsRequired(false);
        }
    }
}