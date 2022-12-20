using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.EfConfigurations
{
    public class PremiumPaymentEfConfiguration : IEntityTypeConfiguration<PremiumPayment>
    {
        public void Configure(EntityTypeBuilder<PremiumPayment> builder)
        {

            builder.HasKey(p => p.IdPayment);
            builder.Property(p => p.IdPayment).IsRequired();
            builder.HasIndex(p => p.IdPayment).IsUnique();

            builder.Property(p => p.IdTransaction).IsRequired(false);

            builder.Property(p => p.PaymentDate).IsRequired(false);

            builder.Property(p => p.Price).IsRequired().HasPrecision(5, 2);

            builder.Property(p => p.PaymentStatus).IsRequired();

            builder.Property(p => p.FirstName).IsRequired(false);

            builder.Property(p => p.LastName).IsRequired(false);

            builder.Property(p => p.Email).IsRequired(false);

            builder.Property(p => p.PaymentUrl).IsRequired(false);
            builder.HasIndex(p => p.PaymentUrl).IsUnique();

            builder.Property(p => p.ValidUntil).IsRequired(false);

            builder.HasOne(p => p.IdUserNavigation)
                .WithMany(u => u.PremiumPayments)
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
