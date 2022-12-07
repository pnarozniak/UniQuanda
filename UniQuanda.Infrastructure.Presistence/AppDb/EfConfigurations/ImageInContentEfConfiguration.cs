using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class ImageInContentEfConfiguration : IEntityTypeConfiguration<ImageInContent>
    {
        public void Configure(EntityTypeBuilder<ImageInContent> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.HasOne(i => i.ImageIdNavigation)
                .WithMany(i => i.ImagesInContent)
                .HasForeignKey(i => i.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.ContentIdNavigation)
                .WithMany(c => c.ImagesInContent)
                .HasForeignKey(i => i.ContentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}