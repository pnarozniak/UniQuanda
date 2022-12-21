using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TestEfConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.CreatedAt).IsRequired();

        builder.Property(t => t.IsFinished).IsRequired().HasDefaultValue(false);

        builder.HasOne(t => t.IdCreatorNavigation)
            .WithMany(c => c.Tests)
            .HasForeignKey(t => t.IdCreator)
            .OnDelete(DeleteBehavior.Cascade);
    }
}