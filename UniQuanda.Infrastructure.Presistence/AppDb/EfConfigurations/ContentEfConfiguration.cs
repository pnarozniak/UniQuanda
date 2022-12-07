using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class ContentEfConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.RawText).IsRequired();
            builder.Property(c => c.ContentType).IsRequired();

            builder.HasOne(c => c.QuestionIdNavigation)
                .WithOne(q => q.ContentIdNavigation)
                .HasForeignKey<Question>(q => q.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.AnswerIdNavigation)
                .WithOne(a => a.ContentIdNavigation)
                .HasForeignKey<Answer>(a => a.ContentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(c => c.ImagesInContent)
                .WithOne(iic => iic.ContentIdNavigation)
                .HasForeignKey(iic => iic.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasGeneratedTsVectorColumn(
                t => t.SearchVector,
                "polish",
                t => t.Text)
                .HasIndex(t => t.SearchVector)
                .HasMethod("GIN");
        }
    }
}