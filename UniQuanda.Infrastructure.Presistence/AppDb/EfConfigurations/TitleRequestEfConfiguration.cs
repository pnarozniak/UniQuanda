using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations;

public class TitleRequestEfConfiguration : IEntityTypeConfiguration<TitleRequest>
{
    public void Configure(EntityTypeBuilder<TitleRequest> builder)
    {
        builder.HasKey(tr => tr.Id);
        builder.Property(tr => tr.Id).ValueGeneratedOnAdd();

        builder.Property(tr => tr.CreatedAt).IsRequired(true);
        builder.Property(tr => tr.AdditionalInfo).IsRequired(false);
        builder.Property(tr => tr.TitleRequestStatus).IsRequired(true);

        builder.HasOne(tr => tr.AcademicTitleIdNavigation)
            .WithMany(at => at.TitleRequests)
            .HasForeignKey(tr => tr.AcademicTitleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.AppIdNavigationUser)
            .WithMany(au => au.TitleRequests)
            .HasForeignKey(tr => tr.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.ScanIdNavigation)
            .WithOne(tr => tr.TitleRequest)
            .HasForeignKey<TitleRequest>(tr => tr.ScanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}