using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class AcademicTitleEfConfiguration : IEntityTypeConfiguration<AcademicTitle>
{
    public void Configure(EntityTypeBuilder<AcademicTitle> builder)
    {
        builder.HasKey(at => at.Id);
        builder.Property(at => at.Id).ValueGeneratedOnAdd();

        builder.HasMany(at => at.UsersTitle)
            .WithOne(ut => ut.AcademicTitleIdNavigation)
            .HasForeignKey(ut => ut.AcademicTitleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(at => at.Name).IsRequired(true);
        builder.Property(at => at.AcademicTitleType);

        builder.HasIndex(at => new { at.AcademicTitleType, at.Name }).IsUnique();
    }
}