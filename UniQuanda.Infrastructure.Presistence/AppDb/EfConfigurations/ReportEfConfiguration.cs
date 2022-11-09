using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
	public class ReportEfConfiguration : IEntityTypeConfiguration<Report>
	{
		public ReportEfConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Description).IsRequired(false).HasMaxLength(1000);
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasOne(r => r.ReporterIdNavigation)
                .WithMany(u => u.CreatedReports)
                .HasForeignKey(u => u.ReporterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ReportedUserIdNavigation)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.ReportedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ReportedQuestionIdNavigation)
                .WithMany(q => q.Reports)
                .HasForeignKey(r => r.ReportedQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ReportedAnswerIdNavigation)
                .WithMany(a => a.Reports)
                .HasForeignKey(r => r.ReportedAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

