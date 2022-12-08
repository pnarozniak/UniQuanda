using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.HandlePremiumPaymentStatus;

public class HandlePremiumPaymentStatusResponseDTO
{
    public HandlePremiumPaymentStatusResultEnum Status { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}