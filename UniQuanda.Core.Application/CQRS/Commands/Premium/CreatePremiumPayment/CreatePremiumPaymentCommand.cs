using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.CreatePremiumPayment;

public class CreatePremiumPaymentCommand : IRequest<CreatePremiumPaymentResponseDTO>
{
    public CreatePremiumPaymentCommand(CreatePremiumPaymentRequestDTO request, int idUser)
    {
        IdUser = idUser;
        IsContinuationPremium = request.IsContinuationPremium!.Value;
    }

    public int IdUser { get; set; }
    public bool IsContinuationPremium { get; set; }
}