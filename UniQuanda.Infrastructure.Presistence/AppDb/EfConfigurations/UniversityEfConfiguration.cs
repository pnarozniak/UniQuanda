using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class UniversityEfConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.HasMany(u => u.AppUsersInUniversity)
            .WithOne(uu => uu.UniversityIdNavigation)
            .HasForeignKey(uu => uu.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Icon).IsRequired();
        builder.Property(u => u.Logo).IsRequired();
        builder.Property(u => u.Contact).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Regex).IsRequired();
    }
}