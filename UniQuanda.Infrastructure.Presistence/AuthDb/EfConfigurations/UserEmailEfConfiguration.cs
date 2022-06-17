using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations
{
    public class UserEmailEfConfiguration : IEntityTypeConfiguration<UserEmail>
    {
        public void Configure(EntityTypeBuilder<UserEmail> builder)
        {
            builder.HasKey(ue => ue.Id);
            builder.Property(ue => ue.Id).ValueGeneratedOnAdd();

            builder.Property(ue => ue.Value).HasMaxLength(320).IsRequired();

            builder.Property(ue => ue.IsMain).IsRequired();
        }
    }
}