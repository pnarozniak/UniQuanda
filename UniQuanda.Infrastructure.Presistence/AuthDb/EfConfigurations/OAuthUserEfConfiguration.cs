using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations;

public class OAuthUserEfConfiguration : IEntityTypeConfiguration<OAuthUser>
{
    public void Configure(EntityTypeBuilder<OAuthUser> builder)
    {
        builder.HasKey(ou => ou.IdUser);

				builder.Property(ou => ou.OAuthId).IsRequired();
				builder.Property(ou => ou.OAuthRegisterConfirmationCode).IsRequired(false);

				builder.HasIndex(ou => new { ou.OAuthId, ou.OAuthProvider }).IsUnique();

				builder.HasOne(ou => ou.IdUserNavigation)
					.WithOne(u => u.IdOAuthUserNavigation)
					.HasForeignKey<OAuthUser>(ou => ou.IdUser)
					.OnDelete(DeleteBehavior.Cascade);
    }
}