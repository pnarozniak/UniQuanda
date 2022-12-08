using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Queries.Premium.GetPremiumPayments;

public class GetPremiumPaymentsRequestDTO
{
    public bool GetAll { get; set; }
}
public class GetPremiumPaymentsResponseDTO
{
    public string Nickname { get; set; }
    public DateTime? HasPremiumUntil { get; set; }
    public IEnumerable<PremiumPaymentInfo> Payments { get; set; }
    public int NumberOfPayments { get; set; }
    public decimal PremiumPrice { get; set; }
}