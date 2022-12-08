using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Premium.HandlePremiumPaymentStatus;

public class HandlePremiumPaymentStatusCommand : IRequest<HandlePremiumPaymentStatusResponseDTO>
{
    public HandlePremiumPaymentStatusCommand(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}