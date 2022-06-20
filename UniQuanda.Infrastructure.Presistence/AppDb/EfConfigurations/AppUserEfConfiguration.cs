using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class AppUserEfConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nickname).HasMaxLength(30).IsRequired();
            builder.HasIndex(u => u.Nickname).IsUnique();

            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired(false);

            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired(false);

            builder.Property(u => u.Birthdate).IsRequired(false);

            builder.Property(u => u.PhoneNumber).HasMaxLength(9).IsRequired(false);

            builder.Property(u => u.City).HasMaxLength(30).IsRequired(false);
        }
    }
}
