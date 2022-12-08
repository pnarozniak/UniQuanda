using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class PremiumPaymentsEntity
{
    public string Nickname { get; set; }
    public DateTime? HasPremiumUntil { get; set; }
    public IEnumerable<PremiumPaymentInfo> Payments { get; set; }
    public int NumberOfPayments { get; set; }
}