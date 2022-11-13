using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class ReportTypeEfConfiguration : IEntityTypeConfiguration<ReportType>
    {
        public ReportTypeEfConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ReportType> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Id).ValueGeneratedOnAdd();

            builder.Property(rt => rt.Name).IsRequired();

            builder.Property(rt => rt.ReportCategory).IsRequired();

            builder.HasMany(rt => rt.Reports)
                .WithOne(r => r.ReportTypeIdNavigation)
                .HasForeignKey(r => r.ReportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

