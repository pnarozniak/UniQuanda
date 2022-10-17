using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AppUserTitleEfConfiguration : IEntityTypeConfiguration<AppUserTitle>
{
    public void Configure(EntityTypeBuilder<AppUserTitle> builder)
    {
        builder.HasKey(ut => ut.Id);
        builder.Property(ut => ut.Id).ValueGeneratedOnAdd();

        builder.Property(uu => uu.Order).IsRequired();

        builder.HasIndex(ut => new { ut.AppUserId, ut.AcademicTitleId }).IsUnique();
        builder.HasIndex(ut => new { ut.AppUserId, ut.Order }).IsUnique();


    }
}