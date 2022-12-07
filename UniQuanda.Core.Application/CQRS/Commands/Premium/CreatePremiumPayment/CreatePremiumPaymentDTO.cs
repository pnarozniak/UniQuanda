using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.CreatePremiumPayment;

public class CreatePremiumPaymentRequestDTO
{
    [Required]
    public bool? IsContinuationPremium { get; set; }
}

public class CreatePremiumPaymentResponseDTO
{
    public CreatePremiumPaymentResultEnum Status { get; set; }
    public string? PaymentUrl { get; set; }
}