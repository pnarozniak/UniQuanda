using UniQuanda.Core.Domain.Enums.DbModel;

namespace UniQuanda.Core.Domain.ValueObjects;

public class PremiumPaymentInfo
{
    public DateTime? PaymentDate { get; set; }
    public string? IdTransaction { get; set; }
    public decimal Price { get; set; }
    public PremiumPaymentStatusEnum PaymentStatus { get; set; }
    public string? PaymentUrl { get; set; }
}