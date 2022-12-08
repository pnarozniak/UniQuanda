using UniQuanda.Core.Domain.Enums.DbModel;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class PremiumPayment
{
    public string IdPayment { get; set; }
    public string? IdTransaction { get; set; }
    public DateTime? PaymentDate { get; set; }
    public Decimal Price { get; set; }
    public PremiumPaymentStatusEnum PaymentStatus { get; set; }
    public int IdUser { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PaymentUrl { get; set; }
    public DateTime? ValidUntil { get; set; }

    public virtual User IdUserNavigation { get; set; }
}