using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TestTagEfConfiguration : IEntityTypeConfiguration<TestTag>
{
    public void Configure(EntityTypeBuilder<TestTag> builder)
    {
        builder.HasKey(tt => tt.Id);
        builder.Property(tt => tt.Id).ValueGeneratedOnAdd();

        builder.HasOne(tt => tt.IdTestNavigation)
            .WithMany(t => t.TestTags)
            .HasForeignKey(tt => tt.IdTest)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tt => tt.IdTagNavigation)
            .WithMany(t => t.TagInTests)
            .HasForeignKey(tt => tt.IdTag)
            .OnDelete(DeleteBehavior.Cascade);
    }
}